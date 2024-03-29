using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.StateMachine;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellFinishState : IState
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IItemsService _itemsService;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IProductionService _productionService;

        private readonly ProductionQueueCellView _cell;

        public ProductionCellFinishState(ProductionQueueCellView cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var data = _cell.Data;
            var item = _itemsProvider.GetItemById(ItemsUtil.ParseItemIdToGroupId(data.Id));
            var sprite = _assetsService.GetSprite(item.SpriteAssetId);
            _cell.SetCellIcon(sprite);
            _cell.SetReadyTimer();
        }

        public void Click()
        {
            var data = _cell.Data;
            var itemBase = _itemsProvider.GetItemById(ItemsUtil.ParseItemIdToGroupId(data.Id));

            var groupData = _itemsService.GetGroupData(itemBase);
            groupData.Experience++;

            var itemData = _itemsService.GetItemData(itemBase.Id);
            itemData.Count++;

            _productionService.CompleteProduction(data.Guid, itemData, groupData);

            _cell.SetStateReady();
            _signalBus.Fire(new ProductionChangeSignal());
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionQueueCellView, ProductionCellFinishState> { }
    }
}