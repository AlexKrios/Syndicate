using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Services
{
    public interface IAssetsService
    {
        AudioClip GetMusicClip(MusicAssetId assetId);

        AudioClip GetAudioClip(AudioAssetId assetId);

        Sprite GetSprite(SpriteAssetId assetId);

        Sprite GetStarSprite(int starCount);
    }
}