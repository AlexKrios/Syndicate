using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellReadyState : IState
    {
        [Inject] private IScreenService _screenService;

        public void Enter() { }

        public void Click()
        {
            _screenService.Show<ProductionViewModel>();
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionCellReadyState> { }
    }
}