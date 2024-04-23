using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "AudioSet", menuName = "Scriptable/Assets/Audio Set", order = -84)]
    public class AudioSetScriptable : ListScriptableObject<AudioAssetScriptable> { }

    [Serializable]
    public class AudioAssetScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;

        public AudioAssetId Id => (AudioAssetId)id;
        public AudioClip AudioClip => audioClip;
    }
}