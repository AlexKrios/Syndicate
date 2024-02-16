using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "Product Set", menuName = "Scriptable/ProductSet")]
    public class ProductGroupSetScriptable : ListScriptableObject<ProductScriptable> { }

    [Serializable]
    public class ProductScriptable
    {
        [SerializeField] private ProductGroupId groupId;
    }
}