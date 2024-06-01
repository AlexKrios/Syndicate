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
    public class ProductionView : ScreenViewBase<ProductionViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
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
        [SerializeField] private Image starIcon;
        [SerializeField] private LocalizeStringEvent itemName;
        //[SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private GameObject specWrapper;
        [SerializeField] private List<SpecificationView> specifications;
        [SerializeField] private List<ProductionPartView> parts;
        [SerializeField] private RequestButton create;
        [SerializeField] private Button starButton;
        [SerializeField] private TMP_Text starCount;

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
        private int CurrentStar
        {
            set
            {
                _currentStar = value <= Constants.MaxStar ? value : 1;
                starCount.text = _currentStar.ToString();
            }
        }

        private void Awake()
        {
            _signalBus.Subscribe<ProductionChangeSignal>(SetCreateButtonState);

            close.onClick.AddListener(CloseClick);
            create.Button.onClick.AddListener(CreateClick);
            starButton.onClick.AddListener(StarClick);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
        }

        private void OnEnable()
        {
            CurrentTab = tabs.First();
            CurrentStar = 1;
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
                .Where(x => ItemsUtil.ParseItemKeyToStar(x.Key) == _currentStar).ToList();

            for (var i = 0; i < productObjects.Count; i++)
            {
                if (products.ElementAtOrDefault(i) == null)
                    products.Add(_componentViewFactory.Create<ProductionProductView>(productsParent));

                var item = productObjects[i];
                var productItems = new List<ICraftableItem> { item };
                foreach (var part in item.Recipe.Parts)
                {
                    var component = _itemsProvider.GetCraftableItemByKey(part.Key);
                    productItems.Add(component);
                }

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
            var data = CurrentItem.Data;
            var recipe = data.Recipe;

            itemIcon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
            starIcon.sprite = _assetsService.GetStarSprite(ItemsUtil.ParseItemKeyToStar(data.Key));
            itemName.StringReference = data.NameLocale;
            //itemDescription.StringReference = data.DescriptionLocale;
            timerValue.text = TimeUtil.DateCraftTimer(recipe.CraftTime);

            SetSpecificationData();

            for (var i = 0; i < parts.Count; i++)
            {
                if (recipe.Parts.ElementAtOrDefault(i) == null)
                {
                    parts[i].SetData(null);
                    continue;
                }

                var part = recipe.Parts[i];
                var partItemObject = _itemsProvider.GetItemByKey(part.Key);
                parts[i].SetData(partItemObject, part.Count);
            }

            SetCreateButtonState();
        }

        private void SetSpecificationData()
        {
            var data = CurrentItem.Data;
            specWrapper.SetActive(data is ProductObject);
            if (data is not ProductObject) return;

            var specificationsList = data.Recipe.Specifications;
            foreach (var specification in specifications)
            {
                var needSpecification = specificationsList.First(x => x.Type == specification.Id);
                specification.SetData(needSpecification);
            }
        }

        private async void CreateClick()
        {
            var data = CurrentItem.Data;
            var productionObject = new ProductionObject
            {
                Guid = Guid.NewGuid(),
                Key = data.Key,
                TimeEnd = DateTime.Now.AddSeconds(data.Recipe.CraftTime).ToFileTime(),
                Index = _productionService.GetFreeCell(),
                ItemRef = data
            };

            await _productionService.AddProduction(productionObject);

            CreateProducts();
            SetSidebarData();
        }

        private void SetCreateButtonState()
        {
            create.Button.interactable = _productionService.IsHaveFreeCell() && _productionService.IsHaveNeedItems(CurrentItem.Data);
        }

        private void StarClick()
        {
            CurrentStar = _currentStar + 1;

            CreateProducts();
            SetTitleData();
            SetSidebarData();
        }
    }
}