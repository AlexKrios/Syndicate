using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Zenject;

namespace Syndicate.Core.View
{
    [UsedImplicitly]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly DiContainer _container;
        private readonly Dictionary<Type, ViewModelBase> _windowModels = new();

        public ViewModelFactory(DiContainer container)
        {
            _container = container;
        }

        public T Build<T>() where T : ViewModelBase
        {
            var type = typeof(T);
            if (_windowModels.ContainsKey(type))
                return (T)_windowModels[type];

            var model = _container.Instantiate<T>();
            _windowModels.Add(type, model);

            return model;
        }
    }
}