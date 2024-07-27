using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionUnitSelectionOutfitView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private ProductGroupId group;
        [SerializeField] private Image icon;

        public ProductGroupId Group => group;

        public void SetData(ItemBaseObject itemBase)
        {
            canvasGroup.alpha = itemBase != null ? NotNullAlpha : NullAlpha;
            icon.gameObject.SetActive(itemBase != null);

            if (itemBase != null)
            {
                icon.sprite = _assetsService.GetSprite(itemBase.SpriteAssetId);
            }
        }
    }
}