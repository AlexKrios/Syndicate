using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class RawId : EntityId<string>, IEquatable<RawId>
    {
        public static readonly RawId Weapon = new("Weapon");
        public static readonly RawId Armor = new("Armor");

        public RawId(string value) : base(value) { }

        public static explicit operator RawId(string value) => new(value);

        public static implicit operator string(RawId groupId) => groupId?.Value;

        public static bool operator ==(RawId a, RawId b) => a?.Value == b?.Value;

        public static bool operator !=(RawId a, RawId b) => !(a == b);

        public bool Equals(RawId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is RawId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}