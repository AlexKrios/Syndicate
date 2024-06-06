using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class StorageView : ScreenViewBase<StorageViewModel>
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IComponentsService _componentsService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private Button close;
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
        [SerializeField] private List<SpecificationView> specifications;
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
            close.onClick.AddListener(CloseClick);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
        }

        private void OnEnable()
        {
            CurrentTab = tabs.First();

            CreateItems();
            SetTitleData();
            SetSidebarData();
        }

        private void CloseClick()
        {
            _screenService.Back();
        }

        private void SetTitleData()
        {
            productGroupType.StringReference = _configurations.GetProductGroupData(CurrentItem.ItemData.ProductGroupId).Locale;
            unitType.StringReference = _configurations.GetUnitTypeData(CurrentItem.ItemData.UnitTypeId).Locale;
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

            var productObjects = _productsService.GetAllProducts().Where(x => x.Count != 0).ToList();
            var componentObjects = _componentsService.GetAllComponents().Where(x => x.Count != 0).ToList();
            var itemObjects = new List<ICraftableItem>();
            itemObjects.AddRange(productObjects);
            itemObjects.AddRange(componentObjects);

            for (var i = 0; i < itemObjects.Count; i++)
            {
                var itemData = itemObjects[i];
                if (CurrentTab.Type != UnitTypeId.All && itemData.UnitTypeId != CurrentTab.Type)
                    continue;

                if (items.ElementAtOrDefault(i) == null)
                    items.Add(_componentViewFactory.Create<StorageItemView>(itemsParent));

                items[i].SetData(itemData);
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
            var data = CurrentItem.ItemData;

            itemIcon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
            var starCount = ItemsUtil.ParseItemKeyToStar(data.Key);
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
                var partItemObject = _itemsProvider.GetItemByKey(part.Key);
                parts[i].SetData(partItemObject);
            }
        }
    }
}