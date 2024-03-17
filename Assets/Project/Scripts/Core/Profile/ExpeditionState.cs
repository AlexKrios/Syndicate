using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class ExpeditionState
    {
        public int Level { get; set; }
        public int Size { get; set; }

        public Dictionary<Guid, ProductionObject> Queue { get; set; } = new();

        public ExpeditionState()
        {
            Level = 1;
            Size = 2;
        }
    }
}