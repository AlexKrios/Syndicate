using JetBrains.Annotations;
using Syndicate.Core.Services;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class ServiceInitializeState : AbstractState, IState
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly ISettingsService _settingsService;

        public async void Enter()
        {
            await _settingsService.Initialize();
            var services = _diContainer.ResolveAll<IService>();
            foreach (var service in services)
            {
                await service.Initialize();
            }

            stateMachine.Enter<ProfileInitializeState>();
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ServiceInitializeState> { }
    }
}