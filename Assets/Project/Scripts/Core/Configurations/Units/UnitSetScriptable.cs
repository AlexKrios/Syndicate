using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UnitSet", menuName = "Scriptable/Units/Unit Set", order = 11)]
    public class UnitSetScriptable : ListScriptableObject<UnitScriptable> { }

    [Serializable]
    public class UnitScriptable
    {
        [SerializeField] protected string name;
        [SerializeField] private string key;
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private SpriteAssetId spriteAssetId;
        [SerializeField] private LocalizedString nameLocale;
        [SerializeField] private LocalizedString descriptionLocale;
        [SerializeField] private List<SpecificationObject> specifications;
        [SerializeField] private GameObject prefabAlly;
        [SerializeField] private GameObject prefabEnemy;

        public string Name { get => name; set => name = value; }
        public UnitId Key { get => (UnitId)key; set => key = value; }
        public UnitTypeId UnitTypeId { get => unitTypeId; set => unitTypeId = value; }
        public SpriteAssetId SpriteAssetId { get => spriteAssetId; set => spriteAssetId = value; }
        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
        public List<SpecificationObject> Specifications => specifications;
        public GameObject PrefabAlly => prefabAlly;
        public GameObject PrefabEnemy => prefabEnemy;
    }
}