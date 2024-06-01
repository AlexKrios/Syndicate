using Syndicate.Core.Configurations;
using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIScreenSetScriptable screens;
        [SerializeField] private UIPopupSetScriptable popups;
        [SerializeField] private UIComponentSetScriptable components;

        [SerializeField] private Transform screenParent;
        [SerializeField] private Transform popupParent;

        public override void InstallBindings()
        {
            Container.Bind<UIScreenSetScriptable>().FromInstance(screens).AsSingle().NonLazy();
            Container.Bind<UIPopupSetScriptable>().FromInstance(popups).AsSingle().NonLazy();
            Container.Bind<UIComponentSetScriptable>().FromInstance(components).AsSingle().NonLazy();

            Container.Bind<Transform>().WithId(Constants.ScreenParent).FromInstance(screenParent).NonLazy();
            Container.Bind<Transform>().WithId(Constants.PopupParent).FromInstance(popupParent).NonLazy();

            Container.BindInterfacesTo<ViewModelFactory>().AsSingle();
            Container.BindInterfacesTo<ScreenViewFactory>().AsSingle();
            Container.BindInterfacesTo<ScreenService>().AsSingle();
            Container.BindInterfacesTo<PopupViewFactory>().AsSingle();
            Container.BindInterfacesTo<PopupService>().AsSingle();
            Container.BindInterfacesTo<ComponentViewFactory>().AsSingle();
            Container.BindInterfacesTo<ViewBuilder>().AsSingle();

            Container.BindFactory<ProductionCellReadyState, ProductionCellReadyState.Factory>();
            Container.BindFactory<ProductionQueueCellView, ProductionCellBusyState, ProductionCellBusyState.Factory>();
            Container.BindFactory<ProductionQueueCellView, ProductionCellFinishState, ProductionCellFinishState.Factory>();
        }
    }
}