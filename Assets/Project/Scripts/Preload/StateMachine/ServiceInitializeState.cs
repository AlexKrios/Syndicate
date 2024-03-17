using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class ServiceInitializeState : AbstractState, IState
    {
        [Inject] private readonly DiContainer _diContainer;

        public async void Enter()
        {
            var services = _diContainer.ResolveAll<IService>();
            foreach (var service in services)
            {
                await service.Initialize();
            }

            SceneManager.LoadScene("Hub");
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ServiceInitializeState> { }
    }
}