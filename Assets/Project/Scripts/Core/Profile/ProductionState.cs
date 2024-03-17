using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class ProductionState
    {
        public int Level { get; set; }
        public int Size { get; set; }

        public Dictionary<Guid, ProductionObject> Queue { get; } = new();

        public ProductionState()
        {
            Level = 1;
            Size = 1;
        }
    }
}