using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Assets
{
    public interface IAssetsService
    {
        AudioClip GetMusicClip(MusicAssetId assetId);

        AudioClip GetAudioClip(AudioAssetId assetId);
    }
}