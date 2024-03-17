using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        private const string ExperienceRoot = "Experience";
        private const string GroupsRoot = "GroupsData";
        private const string ItemsRoot = "ItemsData";
        private const string ProductionSizeRoot = "Production/Size";
        private const string ProductionLevelRoot = "Production/Level";
        private const string ProductionQueueRoot = "Production/Queue";

        private static FirebaseDatabase FirebaseDatabase => FirebaseDatabase.DefaultInstance;

        public ApiService()
        {
            FirebaseDatabase.SetPersistenceEnabled(false);
        }

        public async UniTask<PlayerProfile> GetPlayerProfile()
        {
            var dataSnapshotTask = FirebaseDatabase.RootReference
                .Child($"{UsersRoot}")
                .GetValueAsync();

            await UniTask.WaitUntil(() => dataSnapshotTask.IsCompleted);
            return dataSnapshotTask.Result.Exists
                ? JsonConvert.DeserializeObject<PlayerProfile>(dataSnapshotTask.Result.GetRawJsonValue())
                : null;
        }

        public async UniTask SetStartPlayerProfile(PlayerProfile profile)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(profile));
        }

        public async UniTask SetExperience(int experience)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{InventoryRoot}/{ExperienceRoot}")
                .SetValueAsync(experience);
        }

        #region Items

        public async UniTask SetCountItems(Dictionary<string, object> items)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{InventoryRoot}/{ItemsRoot}")
                .UpdateChildrenAsync(items);
        }

        #endregion

        #region Production

        public async UniTask AddProduction(Guid id, ProductionObject data)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{ProductionQueueRoot}/{id.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }

        public async UniTask RemoveProduction(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{ProductionQueueRoot}/{id.ToString()}")
                .SetValueAsync(null);
        }

        public async UniTask CompleteProduction(ItemData itemData, GroupData groupData)
        {
            var sendList = new Dictionary<string, object>
            {
                { $"{UsersRoot}/{InventoryRoot}/{GroupsRoot}/{groupData.Id}", groupData.ToDictionary() },
                { $"{UsersRoot}/{InventoryRoot}/{ItemsRoot}/{itemData.Id}", itemData.ToDictionary() }
            };

            await FirebaseDatabase.RootReference.UpdateChildrenAsync(sendList);
        }

        public async UniTask SetProductionLevel(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{ProductionLevelRoot}")
                .SetValueAsync(value);
        }

        public async UniTask SetProductionSize(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{ProductionSizeRoot}")
                .SetValueAsync(value);
        }

        #endregion
    }
}