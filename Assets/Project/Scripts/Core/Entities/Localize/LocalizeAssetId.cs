using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class LocalizeAssetId : EntityId<string>, IEquatable<LocalizeAssetId>
    {
        public LocalizeAssetId(string value) : base(value) { }

        public static explicit operator LocalizeAssetId(string value) => new(value);

        public static implicit operator string(LocalizeAssetId id) => id?.Value;

        public static bool operator ==(LocalizeAssetId a, LocalizeAssetId b) => a?.Value == b?.Value;

        public static bool operator !=(LocalizeAssetId a, LocalizeAssetId b) => !(a == b);

        public bool Equals(LocalizeAssetId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is LocalizeAssetId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}