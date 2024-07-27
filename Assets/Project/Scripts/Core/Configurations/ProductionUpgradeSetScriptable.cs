using System;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ProductionUpgradeSet", menuName = "Scriptable/Production Upgrade Set", order = 42)]
    public class ProductionUpgradeSetScriptable : ListScriptableObject<ProductionUpgradeScriptable> { }

    [Serializable]
    public class ProductionUpgradeScriptable
    {
        [SerializeField] private int number;
        [SerializeField] private int level;
        [SerializeField] private int cost;

        public int Number => number;
        public int Level => level;
        public int Cost => cost;
    }
}