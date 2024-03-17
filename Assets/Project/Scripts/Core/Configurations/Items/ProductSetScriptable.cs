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
        [SerializeField] private RecipeObject recipe;

        public ProductId Key { get => (ProductId)key; set => key = value; }
        public ProductGroupId ProductGroupId { get => productGroupId; set => productGroupId = value; }
        public UnitTypeId UnitTypeId { get => unitTypeId; set => unitTypeId = value; }
        public RecipeObject Recipe => recipe;
    }
}