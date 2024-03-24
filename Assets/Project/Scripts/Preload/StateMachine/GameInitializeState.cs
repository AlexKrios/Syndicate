using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class GameInitializeState : AbstractState, IState
    {
        [Inject] private readonly IGameService _gameService;
        public void Enter()
        {
            _gameService.CreatePlayerProfile();

            stateMachine.Enter<ServiceInitializeState>();
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<GameInitializeState> { }
    }
}