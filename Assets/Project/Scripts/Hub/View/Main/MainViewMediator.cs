using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class MainViewMediator : MediatorBase<MainViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        private ProductionSectionView _productionSection;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<MainViewModel>();
            base.Initialize();

            _productionSection = viewModel.ProductionSection;
        }
    }
}