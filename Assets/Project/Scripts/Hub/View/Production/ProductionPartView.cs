using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ProductionPartView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;

        public void SetData(ItemBaseObject item, int needCount = 0)
        {
            icon.gameObject.SetActive(item != null);
            count.gameObject.SetActive(item != null);
            canvasGroup.alpha = item != null ? NotNullAlpha : NullAlpha;

            if (item != null)
            {
                icon.sprite = _assetsService.GetSprite(item.SpriteAssetId);
                count.text = $"{needCount}/{item.Count}";
            }
        }
    }
}