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

        public ComponentId Id => (ComponentId)id;
        public ProductGroupId ProductGroupId => productGroupId;
        public UnitTypeId UnitTypeId => unitTypeId;
    }
}