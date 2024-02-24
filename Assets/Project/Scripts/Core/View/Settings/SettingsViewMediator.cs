using Zenject;

namespace Syndicate.Core.View
{
    public class SettingsViewMediator : MediatorBase<SettingsViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<SettingsViewModel>();
            base.Initialize();
        }
    }
}