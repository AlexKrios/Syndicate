using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using UnityEngine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class GameInitializeState : PreloadState, IState
    {
        public void Enter()
        {
            Application.targetFrameRate = 500;

            stateMachine.SetLoadingPercent(20);
            stateMachine.Enter<ServiceInitializeState>();
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<GameInitializeState> { }
    }
}