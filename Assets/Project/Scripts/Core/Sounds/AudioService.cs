using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Object = UnityEngine.Object;

namespace Syndicate.Core.Sounds
{
    [UsedImplicitly]
    public class AudioService : IAudioService, IService
    {
        private const string AudioServiceName = "----- AudioService -----";
        private const string AudioSourceName = "AudioSource";

        [Inject] private readonly Settings _settings;
        [Inject] private readonly ISettingsService _settingsService;
        [Inject] private readonly IAssetsService _assetsService;

        private Transform _parentAudioService;
        private readonly List<AudioSourceObject> _audioSourceList = new();

        public UniTask Initialize()
        {
            _parentAudioService = new GameObject(AudioServiceName).transform;

            Object.DontDestroyOnLoad(_parentAudioService.gameObject);

            return UniTask.CompletedTask;
        }

        public void Play(AudioAssetId assetId)
        {
            if (_audioSourceList.Count == 0)
                CreateAudioSource();

            var freeAudioSource = FindFreeAudioSource() ?? CreateAudioSource();

            var audioClip = _assetsService.GetAudioClip(assetId);
            freeAudioSource.SetVolume(_settingsService.AudioVolume / 100f);
            freeAudioSource.Play(audioClip);
        }

        private AudioSourceObject FindFreeAudioSource()
        {
            return _audioSourceList.FirstOrDefault(x => !x.IsPlaying);
        }

        private AudioSourceObject CreateAudioSource()
        {
            var audioSourceGameObject = new GameObject(AudioSourceName);
            var audioSource = audioSourceGameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = _settings.MixerGroup;
            audioSource.transform.SetParent(_parentAudioService);

            var audioSourceObject = new AudioSourceObject(audioSource);
            _audioSourceList.Add(audioSourceObject);

            return audioSourceObject;
        }

        public void SetVolume(float value)
        {
            _settingsService.SetAudioVolume(value);
            _audioSourceList.ForEach(x => x.SetVolume(value / 100f));
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private AudioMixerGroup mixerGroup;

            public AudioMixerGroup MixerGroup => mixerGroup;
        }
    }
}