using Syndicate.Core.Entities;

namespace Syndicate.Core.Sounds
{
    public interface IAudioService
    {
        void Play(AudioAssetId assetId);

        void SetVolume(float value);
    }
}