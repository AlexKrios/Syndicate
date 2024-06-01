using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class UnitsState
    {
        public int RosterSize { get; set; }
        public Dictionary<string, UnitDto> Roster { get; } = new();
    }
}