using Zenject;

namespace Syndicate.Core.View
{
    public class LoadingViewMediator : MediatorBase<LoadingViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<LoadingViewModel>();
            base.Initialize();
        }
    }
}