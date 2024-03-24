using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsService _itemsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IProductionSectionFactory _productionSectionFactory;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private LocalizeStringEvent productGroupType;
        [SerializeField] private LocalizeStringEvent unitType;
        [SerializeField] private List<ProductionTabView> tabs;

        [Header("Products")] [SerializeField] private Transform productsParent;
        [SerializeField] private List<ProductionProductView> products;

        [Header("Sidebar")]
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text timerValue;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private List<ProductionSpecView> specifications;
        [SerializeField] private List<ProductionPartView> parts;
        [SerializeField] private Button create;

        [Header("Queue")]
        [SerializeField] private Button queueButton;
        [SerializeField] private GameObject queueObject;

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
            _signalBus.Subscribe<ProductionChangeSignal>(SetCreateButtonState);

            create.onClick.AddListener(CreateClick);
            queueButton.onClick.AddListener(() => queueObject.SetActive(true));

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
            CurrentTab = tabs.First();

            CreateProducts();
            SetTitleData();
            SetSidebarData();
        }

        private void Start()
        {
            SetCreateButtonState();
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

            var unitTabType = CurrentTab.Type;
            var productObjects = unitTabType == UnitTypeId.All
                ? _productsService.GetAllProducts()
                : _productsService.GetProductsByUnitType(unitTabType);

            for (var i = 0; i < productObjects.Count; i++)
            {
                if (products.ElementAtOrDefault(i) == null)
                    products.Add(_productionSectionFactory.CreateProduct(productsParent));

                var productItems = new List<ICraftableItem> { productObjects[i] };
                var componentIds = productObjects[i].Recipe.Parts.Select(x => x.Key).ToList();
                foreach (var componentId in componentIds)
                {
                    var component = _itemsProvider.GetItem<ComponentObject>(componentId);
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
            itemName.StringReference = data.NameLocale;
            itemDescription.StringReference = data.DescriptionLocale;

            var recipe = data.Recipe;
            timerValue.text = TimeUtil.DateCraftTimer(recipe.CraftTime);
            var specificationsList = data is ProductObject
                ? _specificationsUtil.GetProductSpecificationValues(data)
                : recipe.Specifications;
            SetSpecificationData(specificationsList);
            SetPartsData(recipe.Parts);
            SetCreateButtonState();
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
                var partItemObject = _itemsProvider.GetItem(part.ItemType, part.Key);
                parts[i].SetData(partItemObject, part.Count);
            }
        }

        private void CreateClick()
        {
            var data = CurrentItem.GroupData;
            var productionObject = new ProductionObject
            {
                Id = Guid.NewGuid(),
                Key = data.Key,
                Type = data.ItemType,
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
    }
}