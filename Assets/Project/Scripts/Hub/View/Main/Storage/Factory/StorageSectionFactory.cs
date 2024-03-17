using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class StorageSectionFactory : IStorageSectionFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        public StorageItemView CreateItem(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<StorageItemView>(_settings.ItemPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject itemPrefab;

            public GameObject ItemPrefab => itemPrefab;
        }
    }
}