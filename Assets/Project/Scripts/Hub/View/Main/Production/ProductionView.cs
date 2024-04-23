using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.View;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionView : ViewBase<ProductionViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsService _itemsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private Button close;
        [SerializeField] private LocalizeStringEvent productGroupType;
        [SerializeField] private LocalizeStringEvent unitType;
        [SerializeField] private List<ProductionTabView> tabs;

        [Header("Products")]
        [SerializeField] private Transform productsParent;
        [SerializeField] private List<ProductionProductView> products;

        [Header("Sidebar")]
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text timerValue;
        [SerializeField] private Image starIcon;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private List<ProductionSpecView> specifications;
        [SerializeField] private List<ProductionPartView> parts;
        [SerializeField] private Button create;
        [SerializeField] private Button star;

        [Header("Queue")]
        [SerializeField] private Button queueButton;
        [SerializeField] private GameObject queueObject;

        private ProductionTabView _currentTab;
        private ProductionItemView _currentItem;
        private int _currentStar;

        private ProductionTabView CurrentTab
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

        private ProductionItemView CurrentItem
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
            _signalBus.Subscribe<ProductionChangeSignal>(SetCreateButtonState);

            close.onClick.AddListener(CloseClick);
            create.onClick.AddListener(CreateClick);
            queueButton.onClick.AddListener(() => queueObject.SetActive(true));
            star.onClick.AddListener(StarClick);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
        }

        private void OnEnable()
        {
            CurrentTab = tabs.First();
            _currentStar = 1;
            CreateProducts();
            SetTitleData();
            SetSidebarData();
        }

        private void CloseClick()
        {
            _screenService.Back();
        }

        private void SetTitleData()
        {
            productGroupType.StringReference = _configurations.GetProductGroupData(CurrentItem.GroupData.ProductGroupId).Locale;
            unitType.StringReference = _configurations.GetUnitTypeData(CurrentItem.GroupData.UnitTypeId).Locale;
        }

        private void OnTabClick(ProductionTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            CreateProducts();
            SetTitleData();
            SetSidebarData();

            _signalBus.Fire(new ProductionChangeSignal());
        }

        private void CreateProducts()
        {
            if (products.Count != 0)
                products.ForEach(x => x.gameObject.SetActive(false));

            var productObjects = _productsService.GetProductsByUnitType(CurrentTab.Type)
                .Where(x => ItemsUtil.ParseItemIdToStar(x.Id) == _currentStar).ToList();

            for (var i = 0; i < productObjects.Count; i++)
            {
                if (products.ElementAtOrDefault(i) == null)
                    products.Add(_componentViewFactory.Create<ProductionProductView>(productsParent));

                var item = productObjects[i];
                _productionService.RecalculateItemParts(item);
                var productItems = new List<ICraftableItem> { item };
                foreach (var part in item.Recipe.Parts)
                {
                    var component = _itemsProvider.GetCraftableItemById(part.Id);
                    productItems.Add(component);
                }

                productItems.ForEach(x => _itemsService.TryAddItem((ItemBaseObject)x));
                products[i].SetData(productItems, OnItemClick);
                products[i].gameObject.SetActive(true);
            }

            CurrentItem = products.First().GetFirstElement();
        }

        private void OnItemClick(ProductionItemView item)
        {
            if (CurrentItem == item) return;

            CurrentItem = item;

            SetTitleData();
            SetSidebarData();

            _signalBus.Fire(new ProductionChangeSignal());
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
            timerValue.text = TimeUtil.DateCraftTimer(recipe.CraftTime);
            var specificationsList = data is ProductObject
                ? _specificationsUtil.GetProductSpecificationValues(data)
                : recipe.Specifications;
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

            for (var i = 0; i < parts.Count; i++)
            {
                if (recipe.Parts.ElementAtOrDefault(i) == null)
                {
                    parts[i].SetData(null);
                    continue;
                }

                var part = recipe.Parts[i];
                var partItemObject = _itemsProvider.GetItemById(part.Id);
                parts[i].SetData(partItemObject, part.Count);
            }

            SetCreateButtonState();
        }

        private void CreateClick()
        {
            var data = CurrentItem.GroupData;
            var productionObject = new ProductionObject
            {
                Guid = Guid.NewGuid(),
                Id = ItemsUtil.ParseItemToId((ItemBaseObject)data),
                TimeEnd = DateTime.Now.AddSeconds(data.Recipe.CraftTime).ToFileTime(),
                Index = _productionService.GetFreeCell(),
                ItemRef = data
            };

            _productionService.AddProduction(productionObject);
            SetSidebarData();
        }

        private void SetCreateButtonState()
        {
            create.interactable = _productionService.IsHaveFreeCell() && _productionService.IsHaveNeedItems(CurrentItem.GroupData);
        }

        private void StarClick()
        {
            var nextStar = _currentStar + 1;
            _currentStar = nextStar <= Constants.MaxStar ? nextStar : 1;

            CreateProducts();
            SetTitleData();
            SetSidebarData();
        }
    }
}