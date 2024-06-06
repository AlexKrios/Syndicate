using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class StoragePartView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;
        [SerializeField] private Image star;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetData(ItemBaseObject itemBase)
        {
            icon.gameObject.SetActive(itemBase != null);
            star.gameObject.SetActive(itemBase != null);
            canvasGroup.alpha = itemBase != null ? NotNullAlpha : NullAlpha;

            if (itemBase != null)
            {
                icon.sprite = _assetsService.GetSprite(itemBase.SpriteAssetId);

                var starCount = ItemsUtil.ParseItemKeyToStar(itemBase.Key);
                star.sprite = _assetsService.GetStarSprite(starCount);
            }
        }
    }
}