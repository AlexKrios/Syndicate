using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ApiService : IApiService
    {
        private const string UsersRoot = "Users";
        private const string InventoryRoot = "Inventory";
        private const string ProfileRoot = "Profile";
        private const string ExperienceRoot = "Experience";
        private const string GroupsDataRoot = "GroupsData";
        private const string ItemsDataRoot = "ItemsData";
        private const string ProductionSizeRoot = "Production/Size";
        private const string ProductionLevelRoot = "Production/Level";
        private const string ProductionQueueRoot = "Production/Queue";

        public static readonly string ItemsPath = $"{UsersRoot}/{InventoryRoot}/{ItemsDataRoot}";

        private static FirebaseDatabase FirebaseDatabase => FirebaseDatabase.DefaultInstance;
        private static string UserId => FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        public ApiService()
        {
            FirebaseDatabase.SetPersistenceEnabled(false);
        }

        public async UniTask<PlayerProfile> GetPlayerProfile()
        {
            var dataSnapshotTask = FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}")
                .GetValueAsync();

            await UniTask.WaitUntil(() => dataSnapshotTask.IsCompleted);
            return dataSnapshotTask.Result.Exists
                ? JsonConvert.DeserializeObject<PlayerProfile>(dataSnapshotTask.Result.GetRawJsonValue())
                : null;
        }

        public async UniTask SetStartPlayerProfile(PlayerProfile profile)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(profile));
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
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{ExperienceRoot}")
                .SetValueAsync(experience);
        }

        #region Items

        public async UniTask CreateCountItems(ItemData itemData)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsDataRoot}/{itemData.Id}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(itemData));
        }

        public async UniTask SetCountItems(Dictionary<string, object> items)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsDataRoot}")
                .UpdateChildrenAsync(items);
        }

        #endregion

        #region Production

        public async UniTask AddProduction(ProductionObject data, Dictionary<string, object> items)
        {
            items.Add($"{UsersRoot}/{UserId}/{ProductionQueueRoot}/{data.Guid.ToString()}", data.ToDictionary());
            await FirebaseDatabase.RootReference.UpdateChildrenAsync(items);
        }

        public async UniTask RemoveProduction(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionQueueRoot}/{id.ToString()}")
                .SetValueAsync(null);
        }

        public async UniTask CompleteProduction(Guid id, ItemData itemData, GroupData groupData)
        {
            var itemDataPath = $"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsDataRoot}/{itemData.Id}";
            var itemSnapshot = await FirebaseDatabase.RootReference.Child(itemDataPath).GetValueAsync();
            if (!itemSnapshot.Exists)
            {
                await FirebaseDatabase.RootReference
                    .Child($"{UsersRoot}/{UserId}/{InventoryRoot}/{ItemsDataRoot}/{itemData.Id}")
                    .SetRawJsonValueAsync(JsonConvert.SerializeObject(itemData));
            }

            var sendList = new Dictionary<string, object>
            {
                { $"{ProductionQueueRoot}/{id.ToString()}", null },
                { $"{InventoryRoot}/{GroupsDataRoot}/{groupData.Id}", groupData.ToDictionary() },
                { $"{InventoryRoot}/{ItemsDataRoot}/{itemData.Id}/Count", itemData.Count }
            };

            await FirebaseDatabase.RootReference.Child($"{UsersRoot}/{UserId}").UpdateChildrenAsync(sendList);
        }

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

        #endregion
    }
}