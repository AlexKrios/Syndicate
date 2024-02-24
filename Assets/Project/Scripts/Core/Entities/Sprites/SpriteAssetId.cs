using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class SpriteAssetId : EntityId<string>, IEquatable<SpriteAssetId>
    {
        public SpriteAssetId(string value) : base(value) { }

        public static explicit operator SpriteAssetId(string value) => new(value);

        public static implicit operator string(SpriteAssetId id) => id?.Value;

        public static bool operator ==(SpriteAssetId a, SpriteAssetId b) => a?.Value == b?.Value;

        public static bool operator !=(SpriteAssetId a, SpriteAssetId b) => !(a == b);

        public bool Equals(SpriteAssetId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is SpriteAssetId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}