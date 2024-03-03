using Syndicate.Core.Assets;
using Syndicate.Core.Localization;
using Syndicate.Core.Sounds;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Scriptable/DI/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private LocalizationService.Settings localizations;
        [SerializeField] private AssetsService.Settings assets;
        [SerializeField] private MusicService.Settings music;
        [SerializeField] private AudioService.Settings audio;

        public override void InstallBindings()
        {
            Container.BindInstance(localizations);
            Container.BindInstance(assets);
            Container.BindInstance(music);
            Container.BindInstance(audio);
        }
    }
}