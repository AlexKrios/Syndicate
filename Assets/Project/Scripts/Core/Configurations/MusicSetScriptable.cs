using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "Music Set", menuName = "Scriptable/MusicSet")]
    public class MusicSetScriptable : ListScriptableObject<MusicScriptable> { }

    [Serializable]
    public class MusicScriptable
    {
        [FormerlySerializedAs("id")] [SerializeField] private SoundAssetId assetId;
    }
}