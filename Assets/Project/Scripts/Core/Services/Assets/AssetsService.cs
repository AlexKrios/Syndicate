using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class AssetsService : IAssetsService, IService
    {
        [Inject] private readonly Settings _settings;

        private Dictionary<MusicAssetId, AudioClip> _musicAssetsIndex;
        private Dictionary<AudioAssetId, AudioClip> _audioAssetsIndex;
        private Dictionary<SpriteAssetId, Sprite> _spriteAssetsIndex;
        private Dictionary<LocalizeAssetId, LocalizedString> _localizeAssetsIndex;

        public UniTask Initialize()
        {
            _musicAssetsIndex = _settings.Music.Items.ToDictionary(x => x.Id, x => x.AudioClip);
            _audioAssetsIndex = _settings.Audio.Items.ToDictionary(x => x.Id, x => x.AudioClip);
            _spriteAssetsIndex = _settings.Sprite.GetAllSprites().ToDictionary(x => x.Id, x => x.Sprite);
            _localizeAssetsIndex = _settings.Localize.Items.ToDictionary(x => x.Id, x => x.LocalizedString);

            return UniTask.CompletedTask;
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

        public Sprite GetSprite(SpriteAssetId assetId)
        {
            return _spriteAssetsIndex.TryGetValue(assetId, out var sprite)
                ? sprite
                : throw new Exception($"Can't find {nameof(SpriteAssetId)} with id {assetId}");
        }

        public Sprite GetStarSprite(int starCount)
        {
            return _settings.Sprite.Stars[starCount - 1].Sprite;
        }

        public LocalizedString GetLocalize(LocalizeAssetId assetId)
        {
            return _localizeAssetsIndex.TryGetValue(assetId, out var localize)
                ? localize
                : throw new Exception($"Can't find {nameof(LocalizeAssetId)} with id {assetId}");
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private MusicSetScriptable music;
            [SerializeField] private AudioSetScriptable audio;
            [SerializeField] private SpriteSetScriptable sprite;
            [SerializeField] private LocalizeSetScriptable localize;

            public MusicSetScriptable Music => music;
            public AudioSetScriptable Audio => audio;
            public SpriteSetScriptable Sprite => sprite;
            public LocalizeSetScriptable Localize => localize;
        }
    }
}