using Zenject;

namespace Syndicate.Core.View
{
    public class ChangeNameViewMediator : MediatorBase<ChangeNameViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<ChangeNameViewModel>();
            base.Initialize();
        }
    }
}