using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class StorageSectionView : MonoBehaviour
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsService _itemsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IStorageSectionFactory _storageSectionFactory;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private LocalizeStringEvent productGroupType;
        [SerializeField] private LocalizeStringEvent unitType;
        [SerializeField] private List<StorageTabView> tabs;

        [Header("Items")]
        [SerializeField] private Transform itemsParent;
        [SerializeField] private List<StorageItemView> items;

        [Header("Sidebar")]
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image starIcon;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private List<StorageSpecView> specifications;
        [SerializeField] private List<StoragePartView> parts;

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
            SetSidebarData();
        }

        private void SetTitleData()
        {
            productGroupType.StringReference = _configurations.GetProductGroupData(CurrentItem.GroupData.ProductGroupId).Locale;
            unitType.StringReference = _configurations.GetUnitTypeData(CurrentItem.GroupData.UnitTypeId).Locale;
        }

        private void OnTabClick(StorageTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            CreateItems();
            SetTitleData();
            SetSidebarData();
        }

        private void CreateItems()
        {
            if (items.Count != 0)
                items.ForEach(x => x.gameObject.SetActive(false));

            var itemObjects = _itemsService.GetAllItems()
                .Where(x => ItemsUtil.GetItemTypeById(x.Id) != ItemType.Raw)
                .Where(x => x.Count != 0)
                .ToList();

            for (var i = 0; i < itemObjects.Count; i++)
            {
                var itemData = itemObjects[i];
                var groupId = ItemsUtil.ParseItemIdToGroupId(itemData.Id);
                var groupData = _itemsProvider.GetCraftableItemById(groupId);
                if (CurrentTab.Type != UnitTypeId.All && groupData.UnitTypeId != CurrentTab.Type) continue;

                if (items.ElementAtOrDefault(i) == null)
                    items.Add(_storageSectionFactory.CreateItem(itemsParent));

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
            SetSidebarData();
        }

        private void SetSidebarData()
        {
            var data = CurrentItem.GroupData;

            itemIcon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
            var starCount = ItemsUtil.ParseItemIdToStar(data.Id);
            starIcon.sprite = _assetsService.GetStarSprite(starCount);
            itemName.StringReference = data.NameLocale;
            itemDescription.StringReference = data.DescriptionLocale;

            var recipe = data.Recipe;
            var specificationsList = data is ProductObject
                ? _specificationsUtil.GetProductSpecificationValues(data)
                : recipe.Specifications;
            SetSpecificationData(specificationsList);
            SetPartsData(recipe.Parts);
        }

        private void SetSpecificationData(IReadOnlyCollection<SpecificationObject> specificationsList)
        {
            foreach (var specification in specifications)
            {
                var needSpecification = specificationsList.FirstOrDefault(x => x.Type == specification.Id);
                if (needSpecification == null)
                {
                    specification.ResetData();
                    continue;
                }

                specification.SetData(needSpecification);
            }
        }

        private void SetPartsData(IReadOnlyList<PartObject> partObjects)
        {
            for (var i = 0; i < parts.Count; i++)
            {
                if (partObjects.ElementAtOrDefault(i) == null)
                {
                    parts[i].SetData(null);
                    continue;
                }

                var part = partObjects[i];
                var partItemObject = _itemsProvider.GetItemById(part.Id);
                parts[i].SetData(partItemObject);
            }
        }
    }
}