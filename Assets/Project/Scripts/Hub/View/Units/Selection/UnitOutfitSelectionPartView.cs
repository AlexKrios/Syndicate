using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class UnitOutfitSelectionPartView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image icon;

        public void SetData(ItemBaseObject itemBase)
        {
            icon.gameObject.SetActive(itemBase != null);
            canvasGroup.alpha = itemBase != null ? NotNullAlpha : NullAlpha;

            if (itemBase != null)
            {
                icon.sprite = _assetsService.GetSprite(itemBase.SpriteAssetId);
            }
        }
    }
}