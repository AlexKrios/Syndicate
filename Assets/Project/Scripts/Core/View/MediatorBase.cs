using Zenject;

namespace Syndicate.Core.View
{
    public class MediatorBase<TModel> : IInitializable where TModel : ViewModelBase
    {
        protected TModel viewModel;

        public virtual void Initialize()
        {
            viewModel.Show += Enable;
            viewModel.Hide += Disable;
        }

        protected virtual void Enable()
        {
            viewModel.GameObject.SetActive(true);
        }

        protected virtual void Disable()
        {
            viewModel.GameObject.SetActive(false);
        }
    }
}