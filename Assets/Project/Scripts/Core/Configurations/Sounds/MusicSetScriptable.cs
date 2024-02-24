using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "MusicSet", menuName = "Scriptable/Assets/Music Set")]
    public class MusicSetScriptable : ScriptableObject
    {
        [SerializeField] private List<MusicAssetScriptable> items;

        public List<MusicAssetScriptable> Items => items;
    }

    [Serializable]
    public class MusicAssetScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;

        public MusicAssetId Id => (MusicAssetId)id;
        public AudioClip AudioClip => audioClip;
    }
}