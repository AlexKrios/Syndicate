using System.Collections.Generic;
using Syndicate.Core.View;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class NavigationSectionView : MonoBehaviour
    {
        [Inject] private IPopupService _popupService;

        [SerializeField] private List<NavigationTabView> tabs;

        private MainViewModel _mainViewModel;
        private NavigationTabView _currentTab;

        private void Awake()
        {
            _mainViewModel = _popupService.Get<MainViewModel>(true);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
        }

        public NavigationTabView CurrentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab != null)
                    _currentTab.SetInactive();

                _currentTab = value;
                _currentTab.SetActive();
            }
        }

        private void OnTabClick(NavigationTabView tab)
        {
            if (CurrentTab == tab) return;

            _mainViewModel.CurrentTabType = tab.TabType;

            CurrentTab = tab;

            //OnClickEvent?.Invoke();
        }
    }
}