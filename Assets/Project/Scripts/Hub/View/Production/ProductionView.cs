using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ProductionView : ScreenViewBase<ProductionViewModel>
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;
        [Inject] private readonly IProductionService _productionService;

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
        [SerializeField] private LocalizeStringEvent itemName;
        //[SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private GameObject specWrapper;
        [SerializeField] private List<SpecificationView> specifications;
        [SerializeField] private List<ProductionPartView> parts;
        [SerializeField] private RequestButton create;

        private ProductionTabView _currentTab;
        private ProductionItemView _currentItem;

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
            close.onClick.AddListener(CloseClick);
            create.Button.onClick.AddListener(CreateClick);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
        }

        private void OnEnable()
        {
            CurrentTab = tabs.First();
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
            productGroupType.StringReference = _configurations.GetProductGroupData(CurrentItem.Data.ProductGroupId).Locale;
            unitType.StringReference = _configurations.GetUnitTypeData(CurrentItem.Data.UnitTypeId).Locale;
        }

        private void OnTabClick(ProductionTabView tab)
        {
            if (CurrentTab == tab)
                return;

            CurrentTab = tab;

            CreateProducts();
            SetTitleData();
            SetSidebarData();
        }

        private void CreateProducts()
        {
            if (products.Count != 0)
                products.ForEach(x => x.gameObject.SetActive(false));

            var productObjects = _productsService.GetProductByUnitKey(CurrentTab.Type)
                .Where(x => x.Type == ItemType.Product)
                .ToList();

            for (var i = 0; i < productObjects.Count; i++)
            {
                if (products.ElementAtOrDefault(i) == null)
                    products.Add(_componentViewFactory.Create<ProductionProductView>(productsParent));

                var product = productObjects[i];
                var productItems = new List<ICraftableItem> { product };
                foreach (var part in product.Parts)
                {
                    var component = _productsService.GetProduct(part);
                    productItems.Add(component);
                }

                products[i].SetData(productItems, OnItemClick);
                products[i].gameObject.SetActive(true);
            }

            CurrentItem = products.First().GetFirstElement();
        }

        private void OnItemClick(ProductionItemView item)
        {
            if (CurrentItem == item)
                return;

            CurrentItem = item;

            SetTitleData();
            SetSidebarData();
        }

        private void SetSidebarData()
        {
            var data = CurrentItem.Data;
            itemIcon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
            itemName.StringReference = data.NameLocale;
            //itemDescription.StringReference = data.DescriptionLocale;
            timerValue.text = TimeUtil.DateCraftTimer(data.CraftTime);

            specWrapper.SetActive(data.Type == ItemType.Product);
            if (data.Type == ItemType.Product)
            {
                var specificationsList = data.Specifications;
                foreach (var specification in specifications)
                {
                    var needSpecification = specificationsList.First(x => x.Type == specification.Id);
                    specification.SetData(needSpecification);
                }
            }

            for (var i = 0; i < parts.Count; i++)
            {
                if (data.Parts.ElementAtOrDefault(i) == null)
                {
                    parts[i].SetData(null);
                    continue;
                }

                var part = data.Parts[i];
                var partPreset = _itemsProvider.GetItem(part);
                parts[i].SetData(partPreset, part.Count);
            }

            SetCreateButtonState();
        }

        private async void CreateClick()
        {
            var data = CurrentItem.Data;
            var productionObject = new ProductionObject
            {
                Guid = Guid.NewGuid(),
                Key = data.Key,
                Type = data.Type,
                TimeEnd = DateTime.Now.AddSeconds(data.CraftTime).ToFileTime(),
                Index = _productionService.GetFreeCell(),
                Preset = data
            };

            await _productionService.AddProduction(productionObject);

            SetSidebarData();
        }

        private void SetCreateButtonState()
        {
            create.Button.interactable = _productionService.IsHaveFreeCell() && _productionService.IsHaveNeedItems(CurrentItem.Data);
        }
    }
}