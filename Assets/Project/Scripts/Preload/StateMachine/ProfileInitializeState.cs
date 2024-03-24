using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class ProfileInitializeState : AbstractState, IState
    {
        [Inject] private readonly IGameService _gameService;

        public async void Enter()
        {
            await _gameService.LoadPlayerProfile();

            SceneManager.LoadScene("Hub");
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProfileInitializeState> { }
    }
}