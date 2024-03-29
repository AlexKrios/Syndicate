using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "LocationSet", menuName = "Scriptable/Location Set", order = 61)]
    public class LocationSetScriptable : ListScriptableObject<LocationScriptable> { }

    [Serializable]
    public class LocationScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private LocalizedString nameLocale;
        [SerializeField] private LocalizedString descriptionLocale;

        public LocationId Id => (LocationId)id;
        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
    }
}