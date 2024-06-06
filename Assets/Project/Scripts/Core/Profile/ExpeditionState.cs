using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class ExpeditionState
    {
        public int Level { get; }
        public int Size { get; }

        public Dictionary<Guid, ExpeditionObject> Queue { get; } = new();

        public ExpeditionState()
        {
            Level = 1;
            Size = 1;
        }
    }
}