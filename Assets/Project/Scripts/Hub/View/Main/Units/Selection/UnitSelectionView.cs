using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class UnitSelectionView : PopupViewBase<UnitSelectionViewModel>
    {
        [Inject] private readonly IApiService _apiService;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private Button close;
        [SerializeField] private Button empty;
        [SerializeField] private Transform productsParent;
        [SerializeField] private List<UnitSelectionCellView> products;

        [Header("Sidebar")]
        [SerializeField] private GameObject sidebarWrapper;
        [SerializeField] private Image icon;
        [SerializeField] private Image star;
        [SerializeField] private List<SpecificationView> specifications;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private Button equip;

        private UnitSelectionCellView _currentProduct;

        private UnitSelectionCellView CurrentProduct
        {
            get => _currentProduct;
            set
            {
                if (_currentProduct != null)
                    _currentProduct.SetInactive();

                _currentProduct = value;

                if (_currentProduct != null)
                    _currentProduct.SetActive();
            }
        }

        protected override void OnBind()
        {
            base.OnBind();

            close.onClick.AddListener(Close);
            empty.onClick.AddListener(EmptyClick);
            equip.onClick.AddListener(EquipClick);
        }

        protected override async void OnEnable()
        {
            base.OnEnable();

            await UniTask.Yield();

            if (products.Count != 0)
                products.ForEach(x => x.gameObject.SetActive(false));

            var productObjects = _productsService.GetAllProducts().Where(x => x.Count != 0).ToList();
            for (var i = 0; i < productObjects.Count; i++)
            {
                var product = productObjects[i];
                if (product.UnitTypeId != ViewModel.CurrentUnit.UnitTypeId
                    || product.ProductGroupId != ViewModel.CurrentProductGroup)
                    continue;

                if (products.ElementAtOrDefault(i) == null)
                    products.Add(_componentViewFactory.Create<UnitSelectionCellView>(productsParent));

                products[i].SetData(product);
                products[i].OnClickEvent += OnProductClick;
                products[i].gameObject.SetActive(true);
            }

            SetSidebarData();
        }

        private void OnDisable()
        {
            CurrentProduct = null;
        }

        private void OnProductClick(UnitSelectionCellView cell)
        {
            if (CurrentProduct == cell) return;

            CurrentProduct = cell;

            SetSidebarData();
        }

        private void SetSidebarData()
        {
            sidebarWrapper.SetActive(CurrentProduct != null);
            if (CurrentProduct == null) return;

            var data = CurrentProduct.Data;
            icon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
            var starCount = ItemsUtil.ParseItemKeyToStar(data.Key);
            star.sprite = _assetsService.GetStarSprite(starCount);
            itemName.StringReference = data.NameLocale;

            var specificationsList = _specificationsUtil.GetProductSpecificationValues(data);
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

        private async void EmptyClick()
        {
            ViewModel.CurrentUnit.Outfit[ViewModel.CurrentProductGroup] = null;

            var sendList = new ProductObject[2];
            sendList[0] = null;
            sendList[1] = ViewModel.CurrentProduct;

            await _apiService.Request(_apiService.SetUnitOutfit(ViewModel.CurrentUnit, sendList), Finish);

            void Finish()
            {
                var model = _screenService.Get<UnitsViewModel>();
                model.ForceUpdate.Invoke();

                ViewModel.Hide?.Invoke();
            }
        }

        private async void EquipClick()
        {
            ViewModel.CurrentUnit.Outfit[ViewModel.CurrentProductGroup] = CurrentProduct.Data.Key;

            var sendList = new ProductObject[2];
            sendList[0] = CurrentProduct.Data;
            sendList[1] = ViewModel.CurrentProduct;

            await _apiService.Request(_apiService.SetUnitOutfit(ViewModel.CurrentUnit, sendList), Finish);

            void Finish()
            {
                var model = _screenService.Get<UnitsViewModel>();
                model.ForceUpdate.Invoke();

                ViewModel.Hide?.Invoke();
            }
        }
    }
}