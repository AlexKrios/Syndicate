using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using UnityEngine;
using UnityEngine.Localization.Components;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionSectionView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IProductionSectionFactory _productionSectionFactory;

        [SerializeField] private LocalizeStringEvent productGroupType;
        [SerializeField] private LocalizeStringEvent unitType;
        [SerializeField] private List<ProductionTabView> tabs;

        [Space]
        [SerializeField] private Transform productsParent;
        [SerializeField] private List<ProductionProductView> products;

        [Space]
        [SerializeField] private ProductionSidebarView sidebar;

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
            _signalBus.Subscribe<ProductionChangeSignal>(sidebar.Refresh);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
            CurrentTab = tabs.First();
        }

        private void Start()
        {
            CreateProducts();
            SetTitleData();
            sidebar.SetData(CurrentItem.Data);
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
            sidebar.SetData(CurrentItem.Data);

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
            sidebar.SetData(CurrentItem.Data);

            _signalBus.Fire(new ProductionChangeSignal());
        }
    }
}