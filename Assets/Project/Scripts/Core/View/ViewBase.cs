using UnityEngine;

namespace Syndicate.Core.View
{
    public class ViewBase<T> : MonoBehaviour where T : ViewModelBase
    {
        public T ViewModel { get; private set; }

        public void Bind(T viewModel)
        {
            ViewModel = viewModel;
            ViewModel.GameObject = gameObject;

            OnBind();
        }

        protected virtual void OnBind() { }
    }
}