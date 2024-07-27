using JetBrains.Annotations;
using Syndicate.Hub.View;
using Syndicate.Hub.View.Main;
using Zenject;

namespace Syndicate.Core.View
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
            _viewModelFactory.Build<UnitsViewModel>();
            _viewModelFactory.Build<UnitOutfitSelectionViewModel>();
            _viewModelFactory.Build<ProductionViewModel>();
            _viewModelFactory.Build<ProductionQueueUpgradeViewModel>();
            _viewModelFactory.Build<ExpeditionQueueUpgradeViewModel>();
            _viewModelFactory.Build<ExpeditionViewModel>();
            _viewModelFactory.Build<OrdersViewModel>();
            _viewModelFactory.Build<OrderCompanyUpgradeViewModel>();
            _viewModelFactory.Build<StorageViewModel>();
        }
    }
}