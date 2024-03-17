using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellBusyState : IState
    {
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IAssetsService _assetsService;

        private readonly ProductionQueueCellView _cell;

        public ProductionCellBusyState(ProductionQueueCellView cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var data = _cell.Data;
            var item = _itemsProvider.GetItem(data.Type, data.Key);
            var sprite = _assetsService.GetSprite(item.SpriteAssetId);
            _cell.SetCellIcon(sprite);

            StartProductionTimer();
        }

        public void Click() { }

        public void Exit() { }

        private async void StartProductionTimer()
        {
            var productionTime = DateUtil.GetTime(_cell.Data.TimeEnd);
            while (productionTime > 0)
            {
                _cell.SetTimerText(DateUtil.DateCraftTimer(productionTime));
                await UniTask.Delay(1000);
                productionTime--;
            }

            _cell.SetStateFinish();
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionQueueCellView, ProductionCellBusyState> { }
    }
}