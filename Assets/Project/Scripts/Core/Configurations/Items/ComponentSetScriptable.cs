using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ComponentSet", menuName = "Scriptable/Items/Component Set", order = 2)]
    public class ComponentSetScriptable : ListScriptableObject<ComponentScriptable> { }

    [Serializable]
    public class ComponentScriptable : ItemScriptable
    {
        [SerializeField] private ProductGroupId productGroupId;
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private RecipeObject recipe;

        public ComponentId Key { get => (ComponentId)key; set => key = value; }
        public ProductGroupId ProductGroupId { get => productGroupId; set => productGroupId = value; }
        public UnitTypeId UnitTypeId { get => unitTypeId; set => unitTypeId = value; }
        public RecipeObject Recipe => recipe;
    }
}