using Syndicate.Core.Services;
using Syndicate.Core.Sounds;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Scriptable/DI/Game Settings", order = -102)]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetsService.Settings assets;
        [SerializeField] private MusicService.Settings music;
        [SerializeField] private AudioService.Settings audio;

        public override void InstallBindings()
        {
            Container.BindInstance(assets);
            Container.BindInstance(music);
            Container.BindInstance(audio);
        }
    }
}