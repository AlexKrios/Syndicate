using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "OrderSet", menuName = "Scriptable/Order Set", order = 50)]
    public class OrderSetScriptable : ScriptableObject
    {
        [SerializeField] private List<OrderGroupUpgradeScriptable> upgrade;

        public List<OrderGroupUpgradeScriptable> Upgrade => upgrade;
    }

    [Serializable]
    public class OrderScriptable
    {

    }

    [Serializable]
    public class OrderGroupUpgradeScriptable
    {
        [SerializeField] private CompanyId companyId;
        [SerializeField] private int level;
        [SerializeField] private int cost;
        [SerializeField] private int refreshTime;
        [SerializeField] private List<OrderUpgradeScriptable> data;

        public CompanyId CompanyId => companyId;
        public int Level => level;
        public int Cost => cost;
        public int RefreshTime => refreshTime;
        public List<OrderUpgradeScriptable> Data => data;
    }

    [Serializable]
    public class OrderUpgradeScriptable
    {
        [SerializeField] private int number;
        [SerializeField] private int level;
        [SerializeField] private int cost;

        public int Number => number;
        public int Level => level;
        public int Cost => cost;
    }
}