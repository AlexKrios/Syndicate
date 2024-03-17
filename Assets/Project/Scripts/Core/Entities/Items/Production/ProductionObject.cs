using System;
using Newtonsoft.Json;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ProductionObject
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public ItemType Type { get; set; }

        public long TimeEnd { get; set; }

        [JsonIgnore]
        public bool IsLoad { get; set; }
        public int Index { get; set; }
    }
}