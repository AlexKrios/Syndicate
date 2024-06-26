﻿using System.Linq;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "Configurations", menuName = "Scriptable/Configurations", order = 999)]
    public class ConfigurationsScriptable : ScriptableObject
    {
        [SerializeField] private ExperienceSetScriptable experienceSet;

        [Header("Units")]
        [SerializeField] private UnitTypeSetScriptable unitTypeSet;
        [SerializeField] private UnitSetScriptable unitSet;

        [Header("Products")]
        [SerializeField] private ProductGroupSetScriptable productGroupSet;
        [SerializeField] private RawSetScriptable rawSet;
        [SerializeField] private ComponentSetScriptable componentSet;
        [SerializeField] private ProductSetScriptable productSet;

        [Header("Locations")]
        [SerializeField] private LocationSetScriptable locationsSet;

        [Header("Production")]
        [SerializeField] private ProductionSetScriptable productionSet;

        public ExperienceSetScriptable ExperienceSet => experienceSet;
        public UnitTypeSetScriptable UnitTypeSet => unitTypeSet;
        public UnitSetScriptable UnitSet => unitSet;
        public RawSetScriptable RawSet => rawSet;
        public ComponentSetScriptable ComponentSet => componentSet;
        public LocationSetScriptable LocationsSet => locationsSet;
        public ProductSetScriptable ProductSet => productSet;
        public ProductionSetScriptable ProductionSet => productionSet;

        public UnitTypeScriptable GetUnitTypeData(UnitTypeId unitTypeId)
        {
            return unitTypeSet.First(x => x.UnitTypeId == unitTypeId);
        }

        public ProductGroupScriptable GetProductGroupData(ProductGroupId productGroupId)
        {
            return productGroupSet.First(x => x.Group == productGroupId);
        }
    }
}