using JetBrains.Annotations;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class ProductionViewMediator : MediatorBase<ProductionViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<ProductionViewModel>();
            base.Initialize();
        }
    }
}