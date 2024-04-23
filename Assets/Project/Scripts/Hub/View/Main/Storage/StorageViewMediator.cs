using JetBrains.Annotations;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class StorageViewMediator : MediatorBase<StorageViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<StorageViewModel>();
            base.Initialize();
        }
    }
}