using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using UniRx;
using UnityEngine;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ApiService : IApiService
    {
        private const string UsersRoot = "Users";
        private const string InventoryRoot = "Inventory";
        private const string CashRoot = "Cash";
        private const string DiamondRoot = "Diamond";
        private const string ProfileRoot = "Profile";
        private const string UnitsRoot = "Units";
        private const string UnitsRosterRoot = "Roster";
        private const string ExperienceRoot = "Experience";
        private const string ItemsRoot = "Items";
        private const string ProductionRoot = "Production";
        private const string ExpeditionRoot = "Expedition";
        private const string TradeRoot = "Trade";
        private const string OrdersRoot = "Orders";

        private static FirebaseDatabase FirebaseDatabase => FirebaseDatabase.DefaultInstance;
        private static string UserId => FirebaseAuth.DefaultInstance.CurrentUser?.UserId;

        public BoolReactiveProperty IsRequestInProgress { get; } = new();

        public ApiService()
        {
            FirebaseDatabase.SetPersistenceEnabled(false);
        }

        public async UniTask Request(UniTask task, Action finishAction = null)
        {
            IsRequestInProgress.Value = true;

            try
            {
                await task;
                finishAction?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }

            IsRequestInProgress.Value = false;
        }

        public async UniTask<PlayerState> GetPlayerProfile()
        {
            var dataSnapshotTask = FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}")
                .GetValueAsync();

            await UniTask.WaitUntil(() => dataSnapshotTask.IsCompleted);
            return dataSnapshotTask.Result.Exists
                ? JsonConvert.DeserializeObject<PlayerState>(dataSnapshotTask.Result.GetRawJsonValue())
                : null;
        }

        public async UniTask SetStartPlayerProfile(PlayerState state)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(state));
        }

        public async UniTask SetPlayerName(string name)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProfileRoot}/Name")
                .SetValueAsync(name);
        }

        public async UniTask SetCashCount(int cash)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{CashRoot}")
                .SetValueAsync(cash);
        }

        public async UniTask SetDiamondCount(int diamond)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{DiamondRoot}")
                .SetValueAsync(diamond);
        }

        public async UniTask SetExperience(int experience)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProfileRoot}/{ExperienceRoot}")
                .SetValueAsync(experience);
        }

        #region Units

        public async UniTask SetUnitOutfit(UnitObject unitData, ProductObject[] items)
        {
            var sendList = new Dictionary<string, object>();
            if (items[0] != null)
            {
                items[0].Count -= 1;
                sendList.Add($"{items[0].Key}", items[0].ToDictionary());
            }

            if (items[1] != null)
            {
                items[1].Count += 1;
                sendList.Add($"{items[1].Key}", items[1].ToDictionary());
            }

            await SetCountItems(sendList);
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{UnitsRoot}/{UnitsRosterRoot}/{unitData.Key}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(unitData.ToDto()));
        }

        #endregion

        #region Items

        public async UniTask SetCountItems(Dictionary<string, object> items)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsRoot}")
                .UpdateChildrenAsync(items);
        }

        #endregion

        #region Production

        public async UniTask SetProductionLevel(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionRoot}/Level")
                .SetValueAsync(value);
        }

        public async UniTask SetProductionSize(int size, int cash)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{ProductionRoot}/Size", size },
                { $"{InventoryRoot}/{CashRoot}", cash }
            };

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

        public async UniTask AddProduction(ProductionObject data, List<ItemBaseObject> itemsToRemove)
        {
            var sendList = new Dictionary<string, object>();
            foreach (var item in itemsToRemove)
            {
                sendList.Add($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsRoot}/{item.Key}", item.ToDictionary());
            }

            sendList.Add($"{UsersRoot}/{UserId}/{ProductionRoot}/Queue/{data.Guid.ToString()}", data.ToDictionary());
            await FirebaseDatabase.RootReference.UpdateChildrenAsync(sendList);
        }

        public async UniTask RemoveProduction(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionRoot}/Queue/{id.ToString()}")
                .SetValueAsync(null);
        }

        public async UniTask CompleteProduction(Guid id, ICraftableItem item)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{ProductionRoot}/Queue/{id.ToString()}", null },
                { $"{InventoryRoot}/{ItemsRoot}/{item.Key}", item.ToDictionary() }
            };

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

        #endregion

        #region Expeditions

        public async UniTask SetExpeditionSize(int size, int cash)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{ExpeditionRoot}/Size", size },
                { $"{InventoryRoot}/{CashRoot}", cash }
            };

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

        public async UniTask AddExpedition(ExpeditionObject data)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionRoot}/Queue/{data.Guid.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }

        public async UniTask RemoveExpedition(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionRoot}/Queue/{id.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(null));
        }

        #endregion

        #region Orders

        public async UniTask SetOrderSize(string company, int size, int cash)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{TradeRoot}/{OrdersRoot}/{company}/Size", size },
                { $"{InventoryRoot}/{CashRoot}", cash }
            };

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

        public async UniTask SetOrders(Dictionary<CompanyId, TradeOrderObject> orders)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{TradeRoot}/{OrdersRoot}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(orders));
        }

        public async UniTask CompleteOrder(OrderObject order, int cash)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{TradeRoot}/{OrdersRoot}/{order.CompanyId}/List/{order.Guid}", null },
                { $"{InventoryRoot}/{CashRoot}", cash }
            };

            foreach (var part in order.Items)
            {
                sendList.Add($"{InventoryRoot}/{ItemsRoot}/{part.Key}/Count", part.Count);
            }

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

        #endregion
    }
}