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

        UniTask SetCashCount(int cash);

        UniTask SetDiamondCount(int diamond);

        UniTask SetExperience(int experience);

        UniTask SetUnitOutfit(UnitObject unitData, ProductObject[] items);

        UniTask SetCountItems(Dictionary<string, object> items);

        UniTask SetProductionSize(int size, int cash);

        UniTask SetProductionLevel(int value);

        UniTask AddProduction(ProductionObject data, List<ItemBaseObject> itemsToRemove);

        UniTask CompleteProduction(Guid id, ICraftableItem item);

        UniTask RemoveProduction(Guid id);

        UniTask SetExpeditionSize(int size, int cash);

        UniTask AddExpedition(ExpeditionObject data);

        UniTask RemoveExpedition(Guid id);

        UniTask SetOrderSize(string company, int size, int cash);

        UniTask SetOrders(Dictionary<CompanyId, TradeOrderObject> orders);

        UniTask CompleteOrder(OrderObject order, int cash);
    }
}