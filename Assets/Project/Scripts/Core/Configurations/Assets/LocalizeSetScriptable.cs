using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "LocalizeSet", menuName = "Scriptable/Assets/Localize Set", order = -42)]
    public class LocalizeSetScriptable : ListScriptableObject<LocalizeAssetScriptable> { }

    [Serializable]
    public class LocalizeAssetScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private LocalizedString localizedString;

        public LocalizeAssetId Id => (LocalizeAssetId)id;
        public LocalizedString LocalizedString => localizedString;
    }
}