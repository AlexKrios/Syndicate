using Syndicate.Core.Entities;

namespace Syndicate.Core.Sounds
{
    public interface IMusicService
    {
        void Play(MusicAssetId assetId);

        void Stop();

        void SetVolume(float value);
    }
}