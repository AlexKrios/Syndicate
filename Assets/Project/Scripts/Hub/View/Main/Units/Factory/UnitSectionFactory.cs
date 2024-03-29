using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class UnitSectionFactory : IUnitSectionFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        public UnitItemView CreateUnit(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<UnitItemView>(_settings.UnitPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject unitPrefab;

            public GameObject UnitPrefab => unitPrefab;
        }
    }
}