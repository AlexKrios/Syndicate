using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using UniRx;

namespace Syndicate.Core.Services
{
    public interface IApiService
    {
        BoolReactiveProperty IsRequestInProgress { get; }

        UniTask Request(UniTask task, Action finishAction = null);

        UniTask<PlayerState> GetPlayerProfile();

        UniTask SetStartPlayerProfile(PlayerState state);

        UniTask SetPlayerName(string name);

        UniTask SetExperience(int experience);

        UniTask SetUnitOutfit(UnitObject unitData, ProductObject[] items);

        UniTask SetCountItems(Dictionary<string, object> items);

        UniTask AddProduction(ProductionObject data, List<ItemBaseObject> itemsToRemove);

        UniTask RemoveProduction(Guid id);

        UniTask CompleteProduction(Guid id, ICraftableItem item);

        UniTask SetProductionLevel(int value);

        UniTask SetProductionSize(int value);

        UniTask AddExpedition(ExpeditionObject data);

        UniTask RemoveExpedition(Guid id);
    }
}