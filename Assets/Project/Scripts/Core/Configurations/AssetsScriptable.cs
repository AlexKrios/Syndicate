using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "Assets", menuName = "Scriptable/Assets")]
    public class AssetsScriptable : ScriptableObject
    {
        [SerializeField] private SoundSetScriptable sounds;

        public SoundSetScriptable Sounds => sounds;

        public void Initialize()
        {
            sounds.Initialize();
        }

        public AudioClip GetAudioClip(SoundAssetId assetId) => sounds.GetAudioClip(assetId);
    }
}