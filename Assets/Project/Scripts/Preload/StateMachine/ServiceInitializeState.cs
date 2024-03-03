using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class ServiceInitializeState : AbstractState, IState
    {
        [Inject] private IPopupService _popupService;

        public void Enter()
        {
            //_popupService.Get<LoadingViewModel>().LoadingPercent.Value = 100;
            //_popupService.Show<SettingsViewModel>();
        }

        public void Exit()
        {

        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ServiceInitializeState> { }
    }
}