using Project.Scripts;
using Syndicate.Core.Assets;
using Syndicate.Core.Configurations;
using Syndicate.Core.Localization;
using Syndicate.Core.Settings;
using Syndicate.Core.Sounds;
using Syndicate.Preload.StateMachine;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ConfigurationsScriptable configurations;

        public override void InstallBindings()
        {
            Container.DeclareSignals();

            Container.Bind<ConfigurationsScriptable>().FromInstance(configurations).AsSingle().NonLazy();

            Container.BindInterfacesTo<AssetsService>().AsSingle();

            Container.BindInterfacesTo<SettingsService>().AsSingle();

            Container.BindInterfacesTo<LocalizationService>().AsSingle();
            Container.BindInterfacesTo<MusicService>().AsSingle();
            Container.BindInterfacesTo<AudioService>().AsSingle();

            Container.BindFactory<ServiceInitializeState, ServiceInitializeState.Factory>();
            Container.BindFactory<GameInitializeState, GameInitializeState.Factory>();
            Container.BindInterfacesAndSelfTo<PreloadStateMachine>().AsSingle();

            Container.BindInterfacesTo<PreloadStarter>().AsSingle();
        }
    }
}