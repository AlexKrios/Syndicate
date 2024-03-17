using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellFinishState : IState
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IApiService _apiService;
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
            var item = _itemsProvider.GetItem(data.Type, data.Key);
            var sprite = _assetsService.GetSprite(item.SpriteAssetId);
            _cell.SetCellIcon(sprite);
            _cell.SetReadyTimer();
        }

        public async void Click()
        {
            var data = _cell.Data;
            await _productionService.RemoveProduction(data.Id);

            var groupData = _itemsService.GetGroupData(data.Type, data.Key);
            groupData.Experience++;

            var itemData = _itemsService.GetItemData(data.Type, data.Key);
            itemData.Count++;

            await _apiService.CompleteProduction(itemData, groupData);

            _cell.SetStateReady();
            _signalBus.Fire(new ProductionChangeSignal());
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionQueueCellView, ProductionCellFinishState> { }
    }
}