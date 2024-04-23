using Syndicate.Preload.StateMachine;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class PreloadStarter : MonoBehaviour
    {
        [Inject] private readonly PreloadStateMachine _preloadStateMachine;

        private void Awake()
        {
            _preloadStateMachine.Initialize();
            _preloadStateMachine.Enter<GameInitializeState>();
        }

        private void OnDestroy()
        {
            _preloadStateMachine.ResetStates();
        }
    }
}