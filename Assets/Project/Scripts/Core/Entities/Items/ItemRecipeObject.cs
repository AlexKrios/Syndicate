using System;
using System.Collections.Generic;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemRecipeObject
    {
        [SerializeField] private int star;
        [SerializeField] private List<PartObject> parts;
        [SerializeField] private List<SpecificationObject> specifications;
        [SerializeField] private int craftTime;
        [SerializeField] private int experience;
        [SerializeField] private int cost;

        public int Star { get => star; set => star = value; }
        public List<PartObject> Parts { get => parts; set => parts = value; }
        public List<SpecificationObject> Specifications { get => specifications; set => specifications = value; }
        public int CraftTime { get => craftTime; set => craftTime = value; }
        public int Experience { get => experience; set => experience = value; }
        public int Cost { get => cost; set => cost = value; }
    }
}