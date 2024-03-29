﻿using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class PreloadStateMachine : AbstractStateMachine, IInitializable
    {
        [Inject] private readonly GameInitializeState.Factory _gameInitializeStateFactory;
        [Inject] private readonly ServiceInitializeState.Factory _serviceInitializeStateFactory;
        [Inject] private readonly ProfileInitializeState.Factory _profileInitializeStateFactory;

        public void Initialize()
        {
            AddState<GameInitializeState>(_gameInitializeStateFactory.Create());
            AddState<ServiceInitializeState>(_serviceInitializeStateFactory.Create());
            AddState<ProfileInitializeState>(_profileInitializeStateFactory.Create());
        }
    }
}