using System;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ExpeditionUpgradeSet", menuName = "Scriptable/Expedition Upgrade Set", order = 43)]
    public class ExpeditionUpgradeSetScriptable : ListScriptableObject<ExpeditionUpgradeScriptable> { }

    [Serializable]
    public class ExpeditionUpgradeScriptable
    {
        [SerializeField] private int number;
        [SerializeField] private int level;
        [SerializeField] private int cost;

        public int Number => number;
        public int Level => level;
        public int Cost => cost;
    }
}