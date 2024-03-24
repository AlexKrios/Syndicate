using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class InventoryState
    {
        public int Cash { get; set; }
        public int Experience { get; set; }

        public Dictionary<string, GroupData> GroupsData { get; } = new();
        public Dictionary<string, ItemData> ItemsData { get; } = new();
    }
}