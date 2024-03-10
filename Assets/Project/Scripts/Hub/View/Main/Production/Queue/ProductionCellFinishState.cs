using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellFinishState : IState
    {
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IAssetsService _assetsService;

        private readonly ProductionQueueCellView _cell;

        public ProductionCellFinishState(ProductionQueueCellView cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var data = _cell.Data;
            var item = _itemsProvider.GetItem((ItemTypeId)data.Type, data.Key);
            var sprite = _assetsService.GetSprite(item.SpriteAssetId);
            _cell.SetCellIcon(sprite);
            _cell.SetReadyTimer();
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionQueueCellView, ProductionCellFinishState> { }
    }
}