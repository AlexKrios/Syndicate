using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemDto
    {
        public string Key { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }

        public int Count { get; set; }
        public int Experience { get; set; }
    }
}