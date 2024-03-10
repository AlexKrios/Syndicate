using Project.Scripts;
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
            Container.DeclareSignals();

            Container.Bind<ConfigurationsScriptable>().FromInstance(configurations).AsSingle().NonLazy();
            Container.Bind<InputLocker>().FromInstance(inputLocker).AsSingle().NonLazy();

            Container.BindInterfacesTo<GameService>().AsSingle();

            Container.BindInterfacesTo<AssetsService>().AsSingle();

            Container.BindInterfacesTo<SettingsService>().AsSingle();

            Container.BindInterfacesTo<MusicService>().AsSingle();
            Container.BindInterfacesTo<AudioService>().AsSingle();

            Container.BindInterfacesTo<RawService>().AsSingle();
            Container.BindInterfacesTo<ComponentsService>().AsSingle();
            Container.BindInterfacesTo<ProductsService>().AsSingle();
            Container.BindInterfacesTo<ItemsProvider>().AsSingle();

            Container.BindInterfacesTo<ProductionService>().AsSingle();

            Container.BindFactory<ServiceInitializeState, ServiceInitializeState.Factory>();
            Container.BindFactory<GameInitializeState, GameInitializeState.Factory>();
            Container.BindInterfacesAndSelfTo<PreloadStateMachine>().AsSingle();

            Container.BindInterfacesTo<PreloadStarter>().AsSingle();

            Container.Bind<SpecificationsUtil>().AsSingle();
        }
    }
}