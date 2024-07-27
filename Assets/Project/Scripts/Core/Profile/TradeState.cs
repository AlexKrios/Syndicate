using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Profile
{
    public class TradeState
    {
        public Dictionary<CompanyId, TradeOrderObject> Orders { get; } = new();
    }
}