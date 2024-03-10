using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ProductSet", menuName = "Scriptable/Items/Product Set", order = 3)]
    public class ProductSetScriptable : ListScriptableObject<ProductScriptable> { }

    [Serializable]
    public class ProductScriptable : ItemScriptable
    {
        [SerializeField] private ProductGroupId productGroupId;
        [SerializeField] private UnitTypeId unitTypeId;

        public ProductId Id => (ProductId)id;
        public ProductGroupId ProductGroupId => productGroupId;
        public UnitTypeId UnitTypeId => unitTypeId;
    }
}