using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionCellBusyState : IState
    {
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IExpeditionService _expeditionService;

        private readonly ExpeditionQueueCellView _cell;

        public ExpeditionCellBusyState(ExpeditionQueueCellView cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var data = _cell.Data;
            var location = _expeditionService.GetLocation(new LocationId(data.Key));
            var sprite = _assetsService.GetSprite(location.IconAssetId);
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
        public class Factory : PlaceholderFactory<ExpeditionQueueCellView, ExpeditionCellBusyState> { }
    }
}