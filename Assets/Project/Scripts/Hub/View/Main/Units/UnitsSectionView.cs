using System.Collections.Generic;
using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public class UnitsSectionView : MonoBehaviour
    {
        [SerializeField] private List<UnitTabView> tabs;

        [Space]
        [SerializeField] private Transform unitsParent;
        [SerializeField] private List<UnitItemView> units;

        private UnitTabView _currentTab;
        private UnitItemView _currentItem;

        private UnitTabView CurrentTab
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
        private UnitItemView CurrentItem
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
    }
}