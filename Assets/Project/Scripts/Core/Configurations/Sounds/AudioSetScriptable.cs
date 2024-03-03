using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "AudioSet", menuName = "Scriptable/Assets/Audio Set")]
    public class AudioSetScriptable : ScriptableObject
    {
        [SerializeField] private List<AudioAssetScriptable> items;

        public List<AudioAssetScriptable> Items => items;
    }

    [Serializable]
    public class AudioAssetScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;

        public AudioAssetId Id => (AudioAssetId)id;
        public AudioClip AudioClip => audioClip;
    }
}