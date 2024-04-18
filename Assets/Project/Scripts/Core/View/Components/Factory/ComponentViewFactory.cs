using System;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.View
{
    [UsedImplicitly]
    public class ComponentViewFactory : IComponentViewFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UIComponentSetScriptable _config;

        public T Create<T>(Transform parent) where T : IComponentView
        {
            var prefab = FindPrefab<T>();
            if (prefab == null)
                throw new NotImplementedException($"Prefab with type {typeof(T).Name} not found");

            var componentViewBase = _container.InstantiatePrefabForComponent<T>(prefab, parent);
            return componentViewBase;
        }

        private ComponentViewBase FindPrefab<T>() where T : IComponentView
        {
            foreach (var componentViewBase in _config.Components)
            {
                if (typeof(T) == componentViewBase.GetType())
                    return (ComponentViewBase) componentViewBase;
            }

            return null;
        }
    }
}