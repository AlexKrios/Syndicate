using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class RawItemId : EntityId<string>, IEquatable<RawItemId>
    {
        public static readonly RawItemId Weapon = new("Weapon");
        public static readonly RawItemId Armor = new("Armor");

        public RawItemId(string value) : base(value) { }

        public static explicit operator RawItemId(string value) => new(value);

        public static implicit operator string(RawItemId groupItemId) => groupItemId?.Value;

        public static bool operator ==(RawItemId a, RawItemId b) => a?.Value == b?.Value;

        public static bool operator !=(RawItemId a, RawItemId b) => !(a == b);

        public bool Equals(RawItemId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is RawItemId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}