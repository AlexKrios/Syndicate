using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using UniRx;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class AuthInitializeState : PreloadState, IState
    {
        [Inject] private readonly IAuthService _authService;
        [Inject] private readonly IPopupService _popupService;

        private readonly CompositeDisposable _disposable = new();

        public void Enter()
        {
            _authService.Status.Subscribe(x =>
            {
                switch (x)
                {
                    case AuthStatus.Success:
                        stateMachine.Enter<ProfileInitializeState>();
                        break;
                    case AuthStatus.Failure:
                        _popupService.Show<AuthViewModel>();
                        break;
                }
            }).AddTo(_disposable);

            _authService.Initialize();
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<AuthInitializeState> { }
    }
}