using JetBrains.Annotations;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class ViewBuilder : IInitializable
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public void Initialize()
        {
            _viewModelFactory.Build<LoadingViewModel>();
            _viewModelFactory.Build<ChangeNameViewModel>();

            _viewModelFactory.Build<MainViewModel>();
            _viewModelFactory.Build<SettingsViewModel>();
            _viewModelFactory.Build<ProductionViewModel>();
            _viewModelFactory.Build<ProductionQueueUpgradeViewModel>();
            _viewModelFactory.Build<StorageViewModel>();
            _viewModelFactory.Build<UnitsViewModel>();
            _viewModelFactory.Build<UnitSelectionViewModel>();
        }
    }
}