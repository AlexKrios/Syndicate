using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    public class PreloadStateMachine : AbstractStateMachine, IInitializable
    {
        [Inject] private readonly ServiceInitializeState.Factory _serviceInitializeStateFactory;
        [Inject] private readonly GameInitializeState.Factory _gameInitializeStateFactory;

        public void Initialize()
        {
            AddState<ServiceInitializeState>(_serviceInitializeStateFactory.Create());
            AddState<GameInitializeState>(_gameInitializeStateFactory.Create());
        }
    }
}