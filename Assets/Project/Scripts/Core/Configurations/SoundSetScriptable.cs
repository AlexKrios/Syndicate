using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [Serializable]
    public class SoundSetScriptable
    {
        [SerializeField] private List<SoundScriptable> music;
        [SerializeField] private List<SoundScriptable> audio;

        private Dictionary<SoundAssetId, AudioClip> _soundAssetsIndex;

        public void Initialize()
        {
            var unitedList = new List<SoundScriptable>();
            unitedList.AddRange(music);
            unitedList.AddRange(audio);

            _soundAssetsIndex = unitedList.ToDictionary(x => x.Id, x => x.AudioClip);
        }

        public AudioClip GetAudioClip(SoundAssetId assetId)
        {
            Initialize();

            return _soundAssetsIndex.TryGetValue(assetId, out var videoClip)
                    ? videoClip
                    : throw new Exception($"Can't find {nameof(SoundAssetId)} with id {assetId}");
        }
    }

    [Serializable]
    public class SoundScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private SoundAssetId dwaawd;

        public SoundAssetId Id => (SoundAssetId)id;
        public AudioClip AudioClip => audioClip;
    }
}