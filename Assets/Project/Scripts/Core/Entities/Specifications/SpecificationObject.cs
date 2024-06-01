using System;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class SpecificationObject
    {
        [SerializeField] private SpecificationId type;
        [SerializeField] private int value;

        public SpecificationId Type { get => type; set => type = value; }
        public int Value { get => value; set => this.value = value; }

        public SpecificationObject() { }

        public SpecificationObject(SpecificationObject spec)
        {
            type = spec.Type;
            value = spec.Value;
        }
    }
}