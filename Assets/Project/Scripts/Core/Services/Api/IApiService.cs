using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IApiService
    {
        UniTask<PlayerProfile> GetPlayerProfile();

        UniTask SetStartPlayerProfile(PlayerProfile profile);

        UniTask SetExperience(int experience);

        UniTask SetCountItems(Dictionary<string, object> items);

        UniTask AddProduction(Guid id, ProductionObject data);

        UniTask RemoveProduction(Guid id);

        UniTask CompleteProduction(ItemData itemData, GroupData groupData);

        UniTask SetProductionLevel(int value);

        UniTask SetProductionSize(int value);
    }
}