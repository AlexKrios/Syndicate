using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class PartData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType ItemType { get; set; }
        public string Key { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["ItemType"] = ItemType.ToString(),
                ["Key"] = Key
            };
        }
    }
}