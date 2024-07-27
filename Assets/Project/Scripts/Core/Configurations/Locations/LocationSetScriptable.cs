using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "LocationSet", menuName = "Scriptable/Location Set", order = 41)]
    public class LocationSetScriptable : ListScriptableObject<LocationScriptable> { }

    [Serializable]
    public class LocationScriptable
    {
        [SerializeField] private string key;
        [SerializeField] private int wayTime;
        [SerializeField] private List<UnitPosObject> enemies;
        [SerializeField] private List<PartObject> rewards;
        [SerializeField] private LocalizedString nameLocale;
        [SerializeField] private LocalizedString descriptionLocale;
        [SerializeField] private SpriteAssetId iconAssetId;

        public LocationId Key => (LocationId)key;
        public int WayTime => wayTime;
        public List<UnitPosObject> Enemies => enemies;
        public List<PartObject> Rewards => rewards;
        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
        public SpriteAssetId IconAssetId => iconAssetId;
    }
}