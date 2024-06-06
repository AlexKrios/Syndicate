using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class UnitsView : ScreenViewBase<UnitsViewModel>
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IPopupService _popupService;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IUnitsService _unitsService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;

        [SerializeField] private Button close;
        [SerializeField] private List<UnitTabView> tabs;

        [Space]
        [SerializeField] private List<UnitOutfitView> outfit;

        [Header("Units")]
        [SerializeField] private Transform unitsParent;
        [SerializeField] private List<UnitItemView> units;

        [Header("Sidebar")]
        [SerializeField] private Image userIcon;
        [SerializeField] private Image userIconBg;
        [SerializeField] private LocalizeStringEvent userName;
        [SerializeField] private List<SpecificationView> specifications;

        private UnitTabView _currentTab;
        private UnitItemView _currentUnit;

        private UnitTabView CurrentTab
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
        private UnitItemView CurrentUnit
        {
            get => _currentUnit;
            set
            {
                if (_currentUnit != null)
                    _currentUnit.SetInactive();

                _currentUnit = value;
                _currentUnit.SetActive();
            }
        }

        protected override void OnBind()
        {
            ViewModel.ForceUpdate += ForceUpdate;
        }

        private void Awake()
        {
            close.onClick.AddListener(CloseClick);

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
        }

        private void OnEnable()
        {
            CurrentTab = tabs.First();
            CreateUnits();
            SetOutfit();
            SetSidebarData();
        }

        private void ForceUpdate()
        {
            SetOutfit();
            SetSidebarData();
        }

        private void CloseClick()
        {
            _screenService.Back();
        }

        private void OnTabClick(UnitTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;
        }

        private void CreateUnits()
        {
            if (units.Count != 0)
                units.ForEach(x => x.gameObject.SetActive(false));

            var unitObjects = _unitsService.GetAllUnits();
            for (var i = 0; i < unitObjects.Count; i++)
            {
                if (units.ElementAtOrDefault(i) == null)
                    units.Add(_componentViewFactory.Create<UnitItemView>(unitsParent));

                units[i].SetData(unitObjects[i]);
                units[i].OnClickEvent += OnUnitClick;
                units[i].gameObject.SetActive(true);
            }

            CurrentUnit = units.First();
        }

        private void OnUnitClick(UnitItemView unit)
        {
            if (CurrentUnit == unit) return;

            CurrentUnit = unit;

            SetOutfit();
            SetSidebarData();
        }

        private void SetOutfit()
        {
            foreach (var cell in outfit)
            {
                var needData = CurrentUnit.Data.Outfit
                    .FirstOrDefault(x => x.Key == cell.Group);

                cell.SetData(needData.Value);
                cell.OnClickEvent += OnOutfitClick;
            }
        }

        private void OnOutfitClick(ProductGroupId groupId, ProductObject product)
        {
            var model = _popupService.Show<UnitOutfitSelectionViewModel>();
            model.CurrentUnit = CurrentUnit.Data;
            model.CurrentProductGroup = groupId;
            model.CurrentProduct = product;
        }

        private void SetSidebarData()
        {
            var data = CurrentUnit.Data;

            userIcon.sprite = _assetsService.GetSprite(data.IconId);
            userIconBg.color = _configurations.UnitTypeSet.First(x => x.UnitTypeId == data.UnitTypeId).BgColor;
            userName.StringReference = data.NameLocale;

            foreach (var specification in specifications)
            {
                var needSpecification = data.Specifications.First(x => x.Type == specification.Id);
                var specCopy = new SpecificationObject(needSpecification);
                foreach (var cell in outfit)
                {
                    if (cell.Data == null)
                        continue;

                    specCopy.Value += cell.Data.Recipe.Specifications.First(y => y.Type == specification.Id).Value;
                }

                specification.SetData(specCopy);
            }
        }
    }
}