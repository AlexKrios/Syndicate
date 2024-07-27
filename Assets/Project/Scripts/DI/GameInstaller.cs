using Syndicate.Core.Configurations;
using Syndicate.Core.Services;
using Syndicate.Core.Sounds;
using Syndicate.Core.Utils;
using Syndicate.Preload.StateMachine;
using Syndicate.Utils;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ConfigurationsScriptable configurations;
        [SerializeField] private InputLocker inputLocker;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignals();

            Container.Bind<ConfigurationsScriptable>().FromInstance(configurations).AsSingle().NonLazy();
            Container.Bind<InputLocker>().FromInstance(inputLocker).AsSingle().NonLazy();

            Container.BindInterfacesTo<AssetsService>().AsSingle();

            Container.BindInterfacesTo<ApiService>().AsSingle();
            Container.BindInterfacesTo<GameService>().AsSingle();
            Container.BindInterfacesTo<AuthService>().AsSingle();

            Container.BindInterfacesTo<SettingsService>().AsSingle();

            Container.BindInterfacesTo<MusicService>().AsSingle();
            Container.BindInterfacesTo<AudioService>().AsSingle();

            Container.BindInterfacesTo<ExperienceService>().AsSingle();

            Container.BindInterfacesTo<UnitsService>().AsSingle();

            Container.BindInterfacesTo<RawService>().AsSingle();
            Container.BindInterfacesTo<ProductsService>().AsSingle();
            Container.BindInterfacesTo<ItemsProvider>().AsSingle();

            Container.BindInterfacesTo<ProductionService>().AsSingle();
            Container.BindInterfacesTo<ExpeditionService>().AsSingle();
            Container.BindInterfacesTo<OrdersConfigurationProvider>().AsSingle();
            Container.BindInterfacesTo<OrdersService>().AsSingle();

            Container.BindFactory<ServiceInitializeState, ServiceInitializeState.Factory>();
            Container.BindFactory<GameInitializeState, GameInitializeState.Factory>();
            Container.BindFactory<AuthInitializeState, AuthInitializeState.Factory>();
            Container.BindFactory<ProfileInitializeState, ProfileInitializeState.Factory>();
            Container.Bind<PreloadStateMachine>().AsSingle();

            Container.Bind<ItemsUtil>().AsSingle();
            Container.Bind<SpecificationsUtil>().AsSingle();
        }
    }
}