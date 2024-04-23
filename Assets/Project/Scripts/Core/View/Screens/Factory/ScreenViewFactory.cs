using System;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.View
{
    [UsedImplicitly]
    public class ScreenViewFactory : IScreenViewFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UIScreenSetScriptable _config;
        [Inject(Id = Constants.ScreenParent)] private readonly Transform _parent;

        public ViewBase<T> Build<T>() where T : ViewModelBase
        {
            var prefab = FindPrefab<T>();
            if (prefab == null)
                throw new NotImplementedException($"Prefab for {typeof(T).Name} model not found");

            var screenViewBase = _container.InstantiatePrefabForComponent<ViewBase<T>>(prefab, _parent);
            screenViewBase.name = prefab.name;
            return screenViewBase;
        }

        private ViewBase<T> FindPrefab<T>() where T : ViewModelBase
        {
            foreach (var popupViewBase in _config.Screens)
            {
                if (IsScreenHasModel<T>(popupViewBase.GetType()))
                    return (ViewBase<T>) popupViewBase;
            }

            return null;
        }

        private bool IsScreenHasModel<T>(Type screenType) where T : ViewModelBase
        {
            return screenType.IsSubclassOf(typeof(ViewBase<T>));
        }
    }
}