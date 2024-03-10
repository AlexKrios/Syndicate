using Newtonsoft.Json;

namespace Syndicate.Core.Profile
{
    public class InventoryState
    {
        [JsonProperty("Cash")]
        public int cash;

        [JsonProperty("Experience")]
        public ExperienceState experience = new();

        [JsonProperty("Raw")]
        public RawState raw = new();

        [JsonProperty("Components")]
        public ComponentsState components = new();

        [JsonProperty("Products")]
        public ProductsState products = new();
    }
}