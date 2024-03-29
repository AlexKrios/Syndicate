using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Services;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class UnitsSectionView : MonoBehaviour
    {
        [Inject] private readonly IUnitsService _unitsService;
        [Inject] private readonly IUnitSectionFactory _unitSectionFactory;

        [SerializeField] private List<UnitTabView> tabs;

        [Space]
        [SerializeField] private Transform unitsParent;
        [SerializeField] private List<UnitItemView> units;

        private UnitTabView _currentTab;
        private UnitItemView _currentUnit;

        private void OnEnable()
        {
            CreateUnits();
        }

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

        private void CreateUnits()
        {
            if (units.Count != 0)
                units.ForEach(x => x.gameObject.SetActive(false));

            var unitObjects = _unitsService.GetAllUnits();
            for (var i = 0; i < unitObjects.Count; i++)
            {
                if (units.ElementAtOrDefault(i) == null)
                    units.Add(_unitSectionFactory.CreateUnit(unitsParent));

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
        }
    }
}