using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Syndicate.Utils.Exceptions;
using Zenject;

namespace Syndicate.Core.View
{
    [UsedImplicitly]
    public class ScreenService : IScreenService
    {
        [Inject] private readonly IViewModelFactory _viewModelFactory;
        [Inject] private readonly IScreenViewFactory _screenViewFactory;

        private readonly List<ViewModelBase> _screens = new();
        private readonly Stack<ViewModelBase> _history = new();

        public T Show<T>() where T : ViewModelBase
        {
            var model = Get<T>();
            model.Show?.Invoke();

            var currentModel = GetLastWindow();
            if (currentModel != null && !currentModel.Equals(model))
            {
                currentModel.Hide.Invoke();
            }
            _history.Push(model);

            return model;
        }

        public T Get<T>(bool onlyModel = false) where T : ViewModelBase
        {
            var model = _viewModelFactory.Build<T>();
            if (model == null) throw new ViewModelNotFoundException(typeof(T));

            if (!_screens.Contains(model) && !onlyModel)
                InstantiateAndBindPrefab(model);

            return model;
        }

        public void Back()
        {
            var currentWindow = _history.Pop();
            currentWindow.Hide.Invoke();

            var previewWindow = GetLastWindow();
            previewWindow.Show.Invoke();
        }

        private ViewModelBase GetLastWindow()
        {
            ViewModelBase lastWindow = null;
            if (_history.Any())
                lastWindow = _history.Peek();

            return lastWindow;
        }

        private void InstantiateAndBindPrefab<T>(T model) where T : ViewModelBase
        {
            var screen = _screenViewFactory.Build<T>();
            screen.Bind(model);

            _screens.Add(model);
        }
    }
}