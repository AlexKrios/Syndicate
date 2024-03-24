using System;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ProductionSet", menuName = "Scriptable/Production Set", order = 41)]
    public class ProductionSetScriptable : ListScriptableObject<ProductionScriptable> { }

    [Serializable]
    public class ProductionScriptable
    {
        [SerializeField] private int number;
        [SerializeField] private int level;
        [SerializeField] private int cost;

        public int Number => number;
        public int Level => level;
        public int Cost => cost;
    }
}