using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
    public class ExpeditionUnitSelectionView : PopupViewBase<ExpeditionUnitSelectionViewModel>
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IUnitsService _unitsService;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private Button close;
        [SerializeField] private Button empty;
        [SerializeField] private Transform unitsParent;
        [SerializeField] private List<ExpeditionUnitSelectionCellView> units;

        [Header("Sidebar")]
        [SerializeField] private GameObject sidebarWrapper;
        [SerializeField] private Image icon;
        [SerializeField] private Image iconBg;
        [SerializeField] private List<SpecificationView> specifications;
        [SerializeField] private LocalizeStringEvent unitName;
        [SerializeField] private LocalizeStringEvent unitGroup;
        [SerializeField] private List<ExpeditionUnitSelectionOutfitView> outfit;
        [SerializeField] private Button equip;

        private ExpeditionUnitSelectionCellView _currentUnit;

        private ExpeditionUnitSelectionCellView CurrentUnit
        {
            get => _currentUnit;
            set
            {
                if (_currentUnit != null)
                    _currentUnit.SetInactive();

                _currentUnit = value;

                if (_currentUnit != null)
                    _currentUnit.SetActive();
            }
        }

        protected override void OnBind()
        {
            base.OnBind();

            close.onClick.AddListener(Close);
            empty.onClick.AddListener(EmptyClick);
            equip.onClick.AddListener(SelectClick);
        }

        protected override async void OnEnable()
        {
            base.OnEnable();

            await UniTask.Yield();

            if (units.Count != 0)
                units.ForEach(x => x.gameObject.SetActive(false));

            var expeditionModel = _screenService.Get<ExpeditionViewModel>();
            var unitObjects = _unitsService.GetAllUnits()
                .Where(x => ViewModel.UnitTypes.Contains(x.UnitTypeId))
                .Where(x => !expeditionModel.Roster.ContainsValue(x.Key))
                .ToList();

            for (var i = 0; i < unitObjects.Count; i++)
            {
                if (units.ElementAtOrDefault(i) == null)
                    units.Add(_componentViewFactory.Create<ExpeditionUnitSelectionCellView>(unitsParent));

                units[i].SetData(unitObjects[i]);
                units[i].OnClickEvent += OnUnitClick;
                units[i].gameObject.SetActive(true);
            }

            SetSidebarData();
        }

        private void OnDisable()
        {
            CurrentUnit = null;
        }

        private void OnUnitClick(ExpeditionUnitSelectionCellView cell)
        {
            if (CurrentUnit == cell) return;

            CurrentUnit = cell;

            SetSidebarData();
        }

        private void SetSidebarData()
        {
            sidebarWrapper.SetActive(CurrentUnit != null);
            if (CurrentUnit == null) return;

            var data = CurrentUnit.Data;
            icon.sprite = _assetsService.GetSprite(data.IconId);
            iconBg.color = _configurations.UnitTypeSet.First(x => x.UnitTypeId == data.UnitTypeId).BgColor;
            unitName.StringReference = data.NameLocale;
            unitGroup.StringReference = _configurations.GetUnitTypeData(CurrentUnit.Data.UnitTypeId).Locale;

            var specificationsList = _specificationsUtil.GetUnitSpecificationValues(data);
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

            foreach (var cell in outfit)
            {
                if (data.Outfit.TryGetValue(cell.Group, out var key))
                {
                    var item = _productsService.GetProductByKey(new ProductId(key));
                    cell.SetData(item);
                }
                else
                {
                    cell.SetData(null);
                }
            }
        }

        private void EmptyClick()
        {
            var screenModel = _screenService.Get<ExpeditionViewModel>();
            var roster = screenModel.Roster;
            if (roster.TryGetValue(ViewModel.CurrentIndex, out _))
            {
                roster[ViewModel.CurrentIndex] = null;
            }
            else
            {
                roster.Add(ViewModel.CurrentIndex, null);
            }

            screenModel.UpdateView.Invoke();
            ViewModel.Hide?.Invoke();
        }

        private void SelectClick()
        {
            var screenModel = _screenService.Get<ExpeditionViewModel>();
            var roster = screenModel.Roster;
            if (roster.TryGetValue(ViewModel.CurrentIndex, out _))
            {
                roster[ViewModel.CurrentIndex] = CurrentUnit.Data.Key;
            }
            else
            {
                roster.Add(ViewModel.CurrentIndex, CurrentUnit.Data.Key);
            }

            screenModel.UpdateView.Invoke();
            ViewModel.Hide?.Invoke();
        }
    }
}