using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionSectionView : MonoBehaviour
    {
        [Inject] private ConfigurationsScriptable _configurations;
        [Inject] private IItemsProvider _itemsProvider;
        [Inject] private IProductsService _productsService;
        [Inject] private IProductionService _productionService;
        [Inject] private IProductionSectionFactory _productionSectionFactory;

        [SerializeField] private LocalizeStringEvent productGroupType;
        [SerializeField] private LocalizeStringEvent characterType;
        [SerializeField] private List<ProductionTabView> tabs;

        [Space]
        [SerializeField] private Transform productsParent;
        [SerializeField] private List<ProductionProductView> products;

        [Space]
        [SerializeField] private ProductionSidebarView sidebar;
        [SerializeField] private Button create;

        [SerializeField] private Button test;
        [SerializeField] private GameObject queue;

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
            create.onClick.AddListener(CreateButtonClick);
        }

        private void Start()
        {
            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
            CurrentTab = tabs.First();

            test.onClick.AddListener(() => queue.SetActive(true));

            CreateProducts();
            SetTitleData();
            sidebar.SetData(CurrentItem.Data);
        }

        private void OnTabClick(ProductionTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            CreateProducts();
            SetTitleData();
            sidebar.SetData(CurrentItem.Data);
        }

        private void CreateProducts()
        {
            if (products.Count != 0)
                products.ForEach(x => x.gameObject.SetActive(false));

            var unitType = CurrentTab.Type;
            var productObjects = unitType == UnitTypeId.All
                ? _productsService.GetAllProducts()
                : _productsService.GetProductsByUnitType(unitType);

            for (var i = 0; i < productObjects.Count; i++)
            {
                if (products.ElementAtOrDefault(i) == null)
                    products.Add(_productionSectionFactory.CreateProduct(productsParent));

                var productItems = new List<ItemObject> { productObjects[i] };
                var componentIds = productObjects[i].Recipe.Parts.Select(x => x.ItemId).ToList();
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
        }

        private void SetTitleData()
        {
            switch (CurrentItem.Data)
            {
                case ComponentObject componentObject:
                    SetTitle(componentObject.ProductGroupId, componentObject.UnitTypeId);
                    break;

                case ProductObject productObject:
                    SetTitle(productObject.ProductGroupId, productObject.UnitTypeId);
                    break;
            }

            void SetTitle(ProductGroupId group, UnitTypeId unit)
            {
                productGroupType.StringReference = _configurations.GetProductGroupData(group).Locale;
                characterType.StringReference = _configurations.GetUnitTypeData(unit).Locale;
            }
        }

        private void CreateButtonClick()
        {
            SetCreateButtonState();
            create.interactable = _productionService.IsHaveFreeCell();
        }

        private void SetCreateButtonState()
        {
            sidebar.OnCreateClick.Invoke(CurrentItem.Data);
        }
    }
}