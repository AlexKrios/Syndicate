using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [Serializable]
    public class UnitStarScriptable
    {
        [SerializeField] private int star;
        [SerializeField] private List<SpecificationObject> specifications;

        public int Star { get => star; set => star = value; }
        public List<SpecificationObject> Specifications => specifications;
    }
}