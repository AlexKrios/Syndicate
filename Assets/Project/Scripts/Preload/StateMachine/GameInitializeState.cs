﻿using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    public class GameInitializeState : AbstractState, IState
    {
        public void Enter() { }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<GameInitializeState> { }
    }
}