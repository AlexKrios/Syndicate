using System.Collections.Generic;
using Newtonsoft.Json;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class InventoryState
    {
        public int Cash { get; set; }
        public int Experience { get; set; }

        [JsonIgnore] public Dictionary<RawId, RawObject> Raw { get; set; } = new();
        [JsonIgnore] public Dictionary<ComponentId, ComponentObject> Components { get; set; } = new();
        [JsonIgnore] public Dictionary<ProductId, ProductObject> Products { get; set; } = new();
        [JsonIgnore] public Dictionary<UnitId, UnitObject> Units { get; set; } = new();

        public Dictionary<string, GroupData> GroupsData { get; } = new();
        public Dictionary<string, ItemData> ItemsData { get; } = new();
    }
}