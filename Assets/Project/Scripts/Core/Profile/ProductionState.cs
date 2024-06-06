using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class ProductionState
    {
        public int Level { get; }
        public int Size { get; }

        public Dictionary<Guid, ProductionObject> Queue { get; } = new();

        public ProductionState()
        {
            Level = 1;
            Size = 1;
        }
    }
}