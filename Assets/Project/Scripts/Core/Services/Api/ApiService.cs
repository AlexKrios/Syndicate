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
        private const string ProfileRoot = "Profile";
        private const string UnitsRoot = "Units";
        private const string UnitsRosterRoot = "Roster";
        private const string ExperienceRoot = "Experience";
        private const string ItemsDataRoot = "ItemsData";
        private const string ProductionSizeRoot = "Production/Size";
        private const string ProductionLevelRoot = "Production/Level";
        private const string ProductionQueueRoot = "Production/Queue";
        private const string ExpeditionSizeRoot = "Expedition/Size";
        private const string ExpeditionQueueRoot = "Expedition/Queue";

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
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsDataRoot}")
                .UpdateChildrenAsync(items);
        }

        #endregion

        #region Production

        public async UniTask SetProductionLevel(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionLevelRoot}")
                .SetValueAsync(value);
        }

        public async UniTask SetProductionSize(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionSizeRoot}")
                .SetValueAsync(value);
        }

        public async UniTask AddProduction(ProductionObject data, List<ItemBaseObject> itemsToRemove)
        {
            var sendList = new Dictionary<string, object>();
            foreach (var item in itemsToRemove)
            {
                sendList.Add($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsDataRoot}/{item.Key}", item.ToDictionary());
            }

            sendList.Add($"{UsersRoot}/{UserId}/{ProductionQueueRoot}/{data.Guid.ToString()}", data.ToDictionary());
            await FirebaseDatabase.RootReference.UpdateChildrenAsync(sendList);
        }

        public async UniTask RemoveProduction(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionQueueRoot}/{id.ToString()}")
                .SetValueAsync(null);
        }

        public async UniTask CompleteProduction(Guid id, ICraftableItem item)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{ProductionQueueRoot}/{id.ToString()}", null },
                { $"{InventoryRoot}/{ItemsDataRoot}/{item.Key}/Count", item.Count }
            };

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

        #endregion

        #region Expeditions

        public async UniTask SetExpeditionSize(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionSizeRoot}")
                .SetValueAsync(value);
        }

        public async UniTask AddExpedition(ExpeditionObject data)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionQueueRoot}/{data.Guid.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }

        public async UniTask RemoveExpedition(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionQueueRoot}/{id.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(null));
        }

        #endregion
    }
}