using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class InventoryState
    {
        public int Cash { get; set; }
        public int Diamond { get; set; }

        public Dictionary<string, ItemDto> Items { get; } = new();
    }
}