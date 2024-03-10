using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ProductGroupSet", menuName = "Scriptable/Items/Product Group Set", order = 0)]
    public class ProductGroupSetScriptable : ListScriptableObject<ProductGroupScriptable> { }

    [Serializable]
    public class ProductGroupScriptable
    {
        [SerializeField] private ProductGroupId group;
        [SerializeField] private LocalizedString locale;

        public ProductGroupId Group => group;
        public LocalizedString Locale => locale;
    }
}