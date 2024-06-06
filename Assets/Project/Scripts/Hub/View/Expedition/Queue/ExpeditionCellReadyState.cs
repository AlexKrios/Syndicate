using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionCellReadyState : IState
    {
        [Inject] private IScreenService _screenService;

        public void Enter() { }

        public void Click()
        {
            _screenService.Show<ExpeditionViewModel>();
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ExpeditionCellReadyState> { }
    }
}