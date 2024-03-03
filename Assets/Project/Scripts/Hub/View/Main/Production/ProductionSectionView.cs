using System.Collections.Generic;
using Syndicate.Core.View;
using TMPro;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionSectionView : MonoBehaviour
    {
        [Inject] private IPopupService _popupService;

        [SerializeField] private TMP_Text characterType;
        [SerializeField] private List<ProductionTabView> tabs;
        [SerializeField] private List<ProductionItemView> items;

        private MainViewModel _mainViewModel;
        private ProductionTabView _currentTab;
        private ProductionItemView _currentItem;

        public ProductionTabView CurrentTab
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

        public ProductionItemView CurrentItem
        {
            get => _currentItem;
            set
            {
                if (_currentItem != null)
                    _currentItem.SetInactive();

                _currentItem = value;
                _currentItem.SetActive();
            }
        }

        private void Awake()
        {
            _mainViewModel = _popupService.Get<MainViewModel>(true);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
            items.ForEach(x => x.OnClickEvent += OnItemClick);
        }

        private void OnTabClick(ProductionTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            characterType.text = tab.Type;
        }

        private void OnItemClick(ProductionItemView item)
        {
            if (CurrentItem == item) return;

            CurrentItem = item;
        }
    }
}