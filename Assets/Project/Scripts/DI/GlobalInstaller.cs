using Syndicate.Core.Configurations;
using Syndicate.Core.Sounds;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private ConfigurationsScriptable configurations;
        [SerializeField] private AssetsScriptable assetsDatabase;

        public override void InstallBindings()
        {
            Container.DeclareSignals();

            Container.Bind<ConfigurationsScriptable>().FromInstance(configurations).AsSingle().NonLazy();
            Container.Bind<AssetsScriptable>().FromInstance(assetsDatabase).AsSingle().NonLazy();

            Container.Bind<MusicController>().AsSingle();
            Container.Bind<AudioController>().AsSingle();
        }
    }
}