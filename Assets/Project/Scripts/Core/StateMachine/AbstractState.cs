using Syndicate.Preload.StateMachine;
using Zenject;

namespace Syndicate.Core.StateMachine
{
    public abstract class AbstractState
    {
        [Inject] protected readonly PreloadStateMachine stateMachine;
    }
}