using System;
using Syndicate.Core.Assets;
using Syndicate.Core.Entities;
using Syndicate.Core.Settings;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Object = UnityEngine.Object;

namespace Syndicate.Core.Sounds
{
    public class MusicService : IMusicService, IInitializable
    {
        private const string MusicServiceName = "----- MusicService -----";

        [Inject] private readonly Settings _settings;
        [Inject] private readonly ISettingsService _settingsService;
        [Inject] private readonly IAssetsService _assetsService;

        private AudioSource _musicSource;

        public void Initialize()
        {
            var musicService = new GameObject(MusicServiceName);
            _musicSource = musicService.AddComponent<AudioSource>();
            _musicSource.outputAudioMixerGroup = _settings.MixerGroup;

            Object.DontDestroyOnLoad(musicService);
        }

        public void Play(MusicAssetId assetId)
        {
            var musicClip = _assetsService.GetMusicClip(assetId);

            _musicSource.clip = musicClip;
            _musicSource.volume = _settingsService.MusicVolume / 100f;
            _musicSource.Play();
        }

        public void Stop()
        {
            _musicSource.Stop();
        }

        public void SetVolume(float value)
        {
            _settingsService.SetMusicVolume(value);
            _musicSource.volume = value / 100f;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private AudioMixerGroup mixerGroup;

            public AudioMixerGroup MixerGroup => mixerGroup;
        }
    }
}