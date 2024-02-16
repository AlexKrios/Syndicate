using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour, IMusicController
    {
        [Inject] private AssetsScriptable _assetsScriptable;

        private AudioSource _musicSource;

        private void Awake()
        {
            _musicSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }

        public void Play(SoundAssetId assetId)
        {
            var audioClip = _assetsScriptable.GetAudioClip(assetId);

            _musicSource.clip = audioClip;
            _musicSource.Play();
        }

        public void Stop()
        {
            _musicSource.Stop();
        }
    }
}