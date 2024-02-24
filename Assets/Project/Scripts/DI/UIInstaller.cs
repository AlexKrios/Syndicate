using Syndicate.Core.View;
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
        }
    }
}