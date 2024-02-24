namespace Syndicate.Core.StateMachine
{
    public interface IState
    {
        void Enter();

        void Exit();
    }
}