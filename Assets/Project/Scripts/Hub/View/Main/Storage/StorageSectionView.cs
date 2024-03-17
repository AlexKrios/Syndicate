using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using UnityEngine.Localization.Components;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class StorageSectionView : MonoBehaviour
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IStorageSectionFactory _storageSectionFactory;

        [SerializeField] private LocalizeStringEvent productGroupType;
        [SerializeField] private LocalizeStringEvent unitType;
        [SerializeField] private List<StorageTabView> tabs;

        [Space]
        [SerializeField] private Transform itemsParent;
        [SerializeField] private List<StorageItemView> items;

        [Space]
        [SerializeField] private StorageSidebarView sidebar;

        private StorageTabView _currentTab;
        private StorageItemView _currentItem;

        private StorageTabView CurrentTab
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
        private StorageItemView CurrentItem
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
            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
            CurrentTab = tabs.First();
        }

        private void Start()
        {
            CreateItems();
            SetTitleData();
            sidebar.SetData(CurrentItem.Data);
        }

        private void OnTabClick(StorageTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            CreateItems();
            SetTitleData();
            sidebar.SetData(CurrentItem.Data);
        }

        private void CreateItems()
        {
            if (items.Count != 0)
                items.ForEach(x => x.gameObject.SetActive(false));

            var unitTabType = CurrentTab.Type;
            var productObjects = unitTabType == UnitTypeId.All
                ? _productsService.GetAllProducts()
                : _productsService.GetProductsByUnitType(unitTabType);

            for (var i = 0; i < productObjects.Count; i++)
            {
                if (items.ElementAtOrDefault(i) == null)
                {
                    var item = _storageSectionFactory.CreateItem(itemsParent);
                    items.Add(item);
                }

                items[i].SetData(productObjects[i]);
                items[i].OnClickEvent += OnItemClick;
                items[i].gameObject.SetActive(true);
            }

            CurrentItem = items.First();
        }

        private void OnItemClick(StorageItemView item)
        {
            if (CurrentItem == item) return;

            CurrentItem = item;

            SetTitleData();
            sidebar.SetData(CurrentItem.Data);
        }

        private void SetTitleData()
        {
            productGroupType.StringReference = _configurations.GetProductGroupData(CurrentItem.Data.ProductGroupId).Locale;
            unitType.StringReference = _configurations.GetUnitTypeData(CurrentItem.Data.UnitTypeId).Locale;
        }
    }
}