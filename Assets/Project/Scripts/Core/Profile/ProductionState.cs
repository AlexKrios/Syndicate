using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class ProductionState
    {
        public int Level { get; set; } = 1;
        public int Size { get; set; } = 1;

        public Dictionary<Guid, ProductionObject> Queue { get; } = new();
    }
}