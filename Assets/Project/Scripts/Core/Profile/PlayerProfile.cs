using Newtonsoft.Json;

namespace Syndicate.Core.Profile
{
    public class PlayerProfile
    {
        [JsonProperty("Inventory")]
        public InventoryState inventory = new();

        [JsonProperty("Production")]
        public ProductionState production = new();
    }
}