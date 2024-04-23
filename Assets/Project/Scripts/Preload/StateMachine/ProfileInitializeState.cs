using JetBrains.Annotations;
using Syndicate.Core.Profile;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using UnityEngine.SceneManagement;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class ProfileInitializeState : AbstractState, IState
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IPopupService _popupService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();

        public async void Enter()
        {
            await _gameService.LoadPlayerProfile();
            if (string.IsNullOrEmpty(PlayerProfile.Profile.Name))
            {
                _popupService.Show<ChangeNameViewModel>();
            }
            else
            {
                SceneManager.LoadScene("Hub");
            }
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProfileInitializeState> { }
    }
}