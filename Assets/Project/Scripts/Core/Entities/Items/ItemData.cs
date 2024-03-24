using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType ItemType { get; set; }
        public string Id { get; set; }
        public int Count { get; set; }
        public List<PartData> Parts { get; set; } = new();

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object> { ["Count"] = Count };
        }
    }
}