using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.Localization.Components;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class StorageSectionView : MonoBehaviour
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IItemsService _itemsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
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
        }

        private void OnEnable()
        {
            CurrentTab = tabs.First();

            CreateItems();
            SetTitleData();
            sidebar.SetData(CurrentItem.GroupData);
        }

        private void OnTabClick(StorageTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            CreateItems();
            SetTitleData();
            sidebar.SetData(CurrentItem.GroupData);
        }

        private void CreateItems()
        {
            if (items.Count != 0)
                items.ForEach(x => x.gameObject.SetActive(false));

            var itemObjects = _itemsService.GetAllItems()
                .Where(x => x.ItemType != ItemType.Raw)
                .Where(x => x.Count != 0)
                .ToList();

            for (var i = 0; i < itemObjects.Count; i++)
            {
                var itemData = itemObjects[i];
                var groupId = ItemsUtil.ParseItemToIds(itemData.Id);
                var groupData = _itemsProvider.GetCraftableItemById(itemData.ItemType, groupId.First());
                if (CurrentTab.Type != UnitTypeId.All && groupData.UnitTypeId != CurrentTab.Type) continue;

                if (items.ElementAtOrDefault(i) == null)
                {
                    var item = _storageSectionFactory.CreateItem(itemsParent);
                    items.Add(item);
                }

                items[i].SetData(itemData, groupData);
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
            sidebar.SetData(CurrentItem.GroupData);
        }

        private void SetTitleData()
        {
            productGroupType.StringReference = _configurations.GetProductGroupData(CurrentItem.GroupData.ProductGroupId).Locale;
            unitType.StringReference = _configurations.GetUnitTypeData(CurrentItem.GroupData.UnitTypeId).Locale;
        }
    }
}