using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using UnityEngine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class GameInitializeState : AbstractState, IState
    {
        [Inject] private readonly IGameService _gameService;
        public void Enter()
        {
            Application.targetFrameRate = 500;

            _gameService.CreatePlayerProfile();

            stateMachine.Enter<ServiceInitializeState>();
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<GameInitializeState> { }
    }
}