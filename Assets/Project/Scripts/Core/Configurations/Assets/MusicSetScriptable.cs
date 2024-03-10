using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "MusicSet", menuName = "Scriptable/Assets/Music Set", order = 2)]
    public class MusicSetScriptable : ListScriptableObject<MusicAssetScriptable> { }

    [Serializable]
    public class MusicAssetScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;

        public MusicAssetId Id => (MusicAssetId)id;
        public AudioClip AudioClip => audioClip;
    }
}