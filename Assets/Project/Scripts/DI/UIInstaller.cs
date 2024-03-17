using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Transform screenParent;
        [SerializeField] private Transform popupParent;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(Constants.ScreenParent).FromInstance(screenParent);
            Container.Bind<Transform>().WithId(Constants.PopupParent).FromInstance(popupParent);

            Container.BindInterfacesTo<ViewModelFactory>().AsSingle();
            Container.BindInterfacesTo<PopupViewFactory>().AsSingle();
            Container.BindInterfacesTo<PopupService>().AsSingle();

            Container.BindInterfacesTo<LoadingViewMediator>().AsSingle();
            Container.BindInterfacesTo<SettingsViewMediator>().AsSingle();

            Container.BindInterfacesTo<MainViewMediator>().AsSingle();
            Container.BindInterfacesTo<ProductionSectionFactory>().AsSingle();
            Container.BindInterfacesTo<StorageSectionFactory>().AsSingle();

            Container.BindFactory<ProductionQueueCellView, ProductionCellLockedState, ProductionCellLockedState.Factory>();
            Container.BindFactory<ProductionQueueCellView, ProductionCellBusyState, ProductionCellBusyState.Factory>();
            Container.BindFactory<ProductionQueueCellView, ProductionCellReadyState, ProductionCellReadyState.Factory>();
            Container.BindFactory<ProductionQueueCellView, ProductionCellFinishState, ProductionCellFinishState.Factory>();
        }
    }
}