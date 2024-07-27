using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class CompanyId : EntityId<string>, IEquatable<CompanyId>
    {
        public static readonly CompanyId Personal = new ("Personal");
        public static readonly CompanyId Company1 = new ("Company1");
        public static readonly CompanyId Company2 = new ("Company2");
        public static readonly CompanyId Company3 = new ("Company3");
        public static readonly CompanyId Company4 = new ("Company4");

        public CompanyId(string value) : base(value) { }

        public static explicit operator CompanyId(string value) => new(value);

        public static implicit operator string(CompanyId id) => id?.Value;

        public static bool operator ==(CompanyId a, CompanyId b) => a?.Value == b?.Value;

        public static bool operator !=(CompanyId a, CompanyId b) => !(a == b);

        public bool Equals(CompanyId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is CompanyId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}