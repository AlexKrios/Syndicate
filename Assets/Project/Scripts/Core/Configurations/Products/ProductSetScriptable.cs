using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ProductSet", menuName = "Scriptable/Product Set", order = 1)]
    public class ProductSetScriptable : ListScriptableObject<ProductScriptable> { }

    [Serializable]
    public class ProductScriptable
    {
        [SerializeField] private string productId;
        [SerializeField] private HeroTypeId heroTypeId;
        [SerializeField] private ProductGroupId groupId;
    }
}