using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.Assets
{
    public class AssetsService : IAssetsService, IInitializable
    {
        [Inject] private readonly Settings _settings;

        private Dictionary<MusicAssetId, AudioClip> _musicAssetsIndex;
        private Dictionary<AudioAssetId, AudioClip> _audioAssetsIndex;

        public void Initialize()
        {
            _musicAssetsIndex = _settings.Music.Items.ToDictionary(x => x.Id, x => x.AudioClip);
            _audioAssetsIndex = _settings.Audio.Items.ToDictionary(x => x.Id, x => x.AudioClip);
        }

        public AudioClip GetMusicClip(MusicAssetId assetId)
        {
            return _musicAssetsIndex.TryGetValue(assetId, out var audioClip)
                ? audioClip
                : throw new Exception($"Can't find {nameof(MusicAssetId)} with id {assetId}");
        }

        public AudioClip GetAudioClip(AudioAssetId assetId)
        {
            return _audioAssetsIndex.TryGetValue(assetId, out var audioClip)
                ? audioClip
                : throw new Exception($"Can't find {nameof(AudioAssetId)} with id {assetId}");
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private MusicSetScriptable music;
            [SerializeField] private AudioSetScriptable audio;

            public MusicSetScriptable Music => music;
            public AudioSetScriptable Audio => audio;
        }
    }
}