using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class NavigationSectionView : MonoBehaviour
    {
        [Inject] private IPopupService _popupService;

        [SerializeField] private List<NavigationTabView> tabs;
        [SerializeField] private Button battleButton;

        private MainViewModel _mainViewModel;
        private NavigationTabView _currentTab;

        private void Awake()
        {
            _mainViewModel = _popupService.Get<MainViewModel>(true);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);

            battleButton.onClick.AddListener(() => SceneManager.LoadScene("Battle"));

            CurrentTab = tabs.First();
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
        }
    }
}