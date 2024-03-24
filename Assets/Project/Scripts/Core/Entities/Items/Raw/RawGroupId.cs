using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class RawGroupId : EntityId<string>, IEquatable<RawGroupId>
    {
        public RawGroupId(string value) : base(value) { }

        public static explicit operator RawGroupId(string value) => new(value);

        public static implicit operator string(RawGroupId groupItemId) => groupItemId?.Value;

        public static bool operator ==(RawGroupId a, RawGroupId b) => a?.Value == b?.Value;

        public static bool operator !=(RawGroupId a, RawGroupId b) => !(a == b);

        public bool Equals(RawGroupId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is RawGroupId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}