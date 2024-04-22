using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class PreloadStateMachine : AbstractStateMachine
    {
        [Inject] private readonly GameInitializeState.Factory _gameInitializeStateFactory;
        [Inject] private readonly ServiceInitializeState.Factory _serviceInitializeStateFactory;
        [Inject] private readonly AuthInitializeState.Factory _authInitializeStateFactory;
        [Inject] private readonly ProfileInitializeState.Factory _profileInitializeStateFactory;

        public void Initialize()
        {
            AddState<GameInitializeState>(_gameInitializeStateFactory.Create());
            AddState<ServiceInitializeState>(_serviceInitializeStateFactory.Create());
            AddState<AuthInitializeState>(_authInitializeStateFactory.Create());
            AddState<ProfileInitializeState>(_profileInitializeStateFactory.Create());
        }
    }
}