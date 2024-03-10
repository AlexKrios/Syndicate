using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ComponentId : EntityId<string>, IEquatable<ComponentId>
    {
        public ComponentId(string value) : base(value) { }

        public static explicit operator ComponentId(string value) => new(value);

        public static implicit operator string(ComponentId groupId) => groupId?.Value;

        public static bool operator ==(ComponentId a, ComponentId b) => a?.Value == b?.Value;

        public static bool operator !=(ComponentId a, ComponentId b) => !(a == b);

        public bool Equals(ComponentId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is ComponentId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}