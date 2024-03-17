using Syndicate.Preload.StateMachine;
using Zenject;

namespace Project.Scripts
{
    public class PreloadStarter : IInitializable
    {
        [Inject] private readonly PreloadStateMachine _preloadStateMachine;

        public void Initialize()
        {
            _preloadStateMachine.Enter<GameInitializeState>();
        }
    }
}