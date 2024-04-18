using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class UnitsState
    {
        public Dictionary<string, ItemData> Roster { get; } = new();
    }
}