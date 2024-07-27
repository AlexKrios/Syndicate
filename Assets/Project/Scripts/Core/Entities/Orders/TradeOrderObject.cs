using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    [Serializable]
    public class TradeOrderObject
    {
        public int Size { get; set; }
        public Dictionary<Guid, OrderObject> List { get; set; } = new();

        public long RefreshTime { get; set; }
    }
}