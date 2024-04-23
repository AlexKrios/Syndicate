using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    public class PreloadState : AbstractState
    {
        [Inject] protected readonly PreloadStateMachine stateMachine;
    }
}