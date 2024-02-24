using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    public class GameInitializeState : AbstractState, IState
    {
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public class Factory : PlaceholderFactory<GameInitializeState> { }
    }
}