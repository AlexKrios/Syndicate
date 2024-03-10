
using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class UnitId : EntityId<string>, IEquatable<UnitId>
    {
        public UnitId(string value) : base(value) { }

        public static explicit operator UnitId(string value) => new(value);

        public static implicit operator string(UnitId groupTypeId) => groupTypeId?.Value;

        public static bool operator ==(UnitId a, UnitId b) => a?.Value == b?.Value;

        public static bool operator !=(UnitId a, UnitId b) => !(a == b);

        public bool Equals(UnitId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is UnitId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}