
using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class UnitSideId : EntityId<string>, IEquatable<UnitSideId>
    {
        public static readonly UnitSideId Ally = new ("Ally");
        public static readonly UnitSideId Enemy = new ("Enemy");

        public UnitSideId(string value) : base(value) { }

        public static explicit operator UnitSideId(string value) => new(value);

        public static implicit operator string(UnitSideId groupTypeId) => groupTypeId?.Value;

        public static bool operator ==(UnitSideId a, UnitSideId b) => a?.Value == b?.Value;

        public static bool operator !=(UnitSideId a, UnitSideId b) => !(a == b);

        public bool Equals(UnitSideId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is UnitSideId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}