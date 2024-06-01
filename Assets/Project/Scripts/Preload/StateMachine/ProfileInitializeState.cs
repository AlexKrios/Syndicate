using JetBrains.Annotations;
using Syndicate.Core.Profile;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class ProfileInitializeState : PreloadState, IState
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IPopupService _popupService;

        private PlayerState PlayerState => _gameService.GetPlayerState();

        public async void Enter()
        {
            await _gameService.LoadPlayerProfile();
            if (string.IsNullOrEmpty(PlayerState.Profile.Name))
            {
                _popupService.Show<ChangeNameViewModel>();
            }
            else
            {
                await stateMachine.SetLoadingFinish();
            }
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProfileInitializeState> { }
    }
}