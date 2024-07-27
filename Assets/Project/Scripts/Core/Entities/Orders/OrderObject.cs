using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Syndicate.Core.Services.Serialization;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class OrderObject
    {
        public Guid Guid { get; set; }

        [JsonConverter(typeof(CompanyIdConverter))]
        public CompanyId CompanyId { get; set; }
        public int Index { get; set; }

        public List<PartObject> Items { get; set; } = new();
    }
}