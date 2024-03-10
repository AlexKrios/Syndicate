using System;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionSectionFactory : IProductionSectionFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        public ProductionProductView CreateProduct(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ProductionProductView>(_settings.ProductPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject productPrefab;

            public GameObject ProductPrefab => productPrefab;
        }
    }
}