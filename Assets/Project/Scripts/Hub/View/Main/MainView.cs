using Syndicate.Core.View;
using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public class MainView : ViewBase<MainViewModel>
    {
        [SerializeField] private NavigationSectionView navigation;

        [Space]
        [SerializeField] private UnitsSectionView units;
        [SerializeField] private ProductionView production;

        protected override void OnBind()
        {
            base.OnBind();

            ViewModel.Production = production;
        }
    }
}