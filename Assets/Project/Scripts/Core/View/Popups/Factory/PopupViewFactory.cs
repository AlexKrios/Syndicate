using System;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.View
{
    [UsedImplicitly]
    public class PopupViewFactory : IPopupViewFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UIPopupSetScriptable _config;
        [Inject(Id = Constants.PopupParent)] private readonly Transform _parent;

        public ViewBase<T> Build<T>() where T : ViewModelBase
        {
            var prefab = FindPrefab<T>();
            if (prefab == null)
                throw new NotImplementedException($"Prefab for {typeof(T).Name} model not found");

            var popupViewBase = _container.InstantiatePrefabForComponent<ViewBase<T>>(prefab, _parent);
            popupViewBase.name = prefab.name;
            SortAllPopups();
            return popupViewBase;
        }

        private void SortAllPopups()
        {
            for (var i = 0; i < _config.Popups.Count; i++)
            {
                var popup = _parent.Find(_config.Popups[i].name);
                if(popup == null) continue;

                popup.SetSiblingIndex(i);
            }
        }

        private ViewBase<T> FindPrefab<T>() where T : ViewModelBase
        {
            foreach (var popupViewBase in _config.Popups)
            {
                if (IsPopupHasModel<T>(popupViewBase.GetType()))
                    return (ViewBase<T>) popupViewBase;
            }

            return null;
        }

        private bool IsPopupHasModel<T>(Type popupType) where T : ViewModelBase
        {
            return popupType.IsSubclassOf(typeof(ViewBase<T>));
        }
    }
}