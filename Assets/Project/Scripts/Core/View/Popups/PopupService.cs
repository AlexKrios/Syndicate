using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Utils.Exceptions;
using Zenject;

namespace Syndicate.Core.View
{
    [UsedImplicitly]
    public class PopupService : IPopupService
    {
        [Inject] private readonly IViewModelFactory _viewModelFactory;
        [Inject] private readonly IPopupViewFactory _popupViewFactory;

        private readonly List<ViewModelBase> _popups = new();

        public T Show<T>() where T : ViewModelBase
        {
            var model = Get<T>();
            model.Show?.Invoke();
            return model;
        }

        public T Get<T>(bool onlyModel = false) where T : ViewModelBase
        {
            var model = _viewModelFactory.Build<T>();
            if (model == null) throw new ViewModelNotFoundException(typeof(T));

            if (!_popups.Contains(model) && !onlyModel)
                InstantiateAndBindPrefab(model);

            return model;
        }

        private void InstantiateAndBindPrefab<T>(T model) where T : ViewModelBase
        {
            var screen = _popupViewFactory.Build<T>();
            screen.Bind(model);

            _popups.Add(model);
        }
    }
}