using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ExpeditionId : EntityId<string>, IEquatable<ExpeditionId>
    {
        public ExpeditionId(string value) : base(value) { }

        public static explicit operator ExpeditionId(string value) => new(value);

        public static implicit operator string(ExpeditionId id) => id?.Value;

        public static bool operator ==(ExpeditionId a, ExpeditionId b) => a?.Value == b?.Value;

        public static bool operator !=(ExpeditionId a, ExpeditionId b) => !(a == b);

        public bool Equals(ExpeditionId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is ExpeditionId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}