using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class SoundAssetId : EntityId<string>, IEquatable<SoundAssetId>
    {
        public SoundAssetId(string value) : base(value) { }

        public static explicit operator SoundAssetId(string value) => new(value);

        public static implicit operator string(SoundAssetId groupAssetId) => groupAssetId?.Value;

        public static bool operator ==(SoundAssetId a, SoundAssetId b) => a?.Value == b?.Value;

        public static bool operator !=(SoundAssetId a, SoundAssetId b) => !(a == b);

        public bool Equals(SoundAssetId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is SoundAssetId id && Equals(id);
    }
}