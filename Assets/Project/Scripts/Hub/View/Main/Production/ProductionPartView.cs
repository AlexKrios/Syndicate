using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionPartView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsService _itemsService;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetData(ItemBaseObject itemObject, int needCount = 0)
        {
            icon.gameObject.SetActive(itemObject != null);
            count.gameObject.SetActive(itemObject != null);
            canvasGroup.alpha = itemObject != null ? NotNullAlpha : NullAlpha;

            if (itemObject != null)
            {
                icon.sprite = _assetsService.GetSprite(itemObject.SpriteAssetId);

                var item = _itemsService.GetItemData(itemObject.ItemType, itemObject.Key);
                count.text = $"{needCount}/{item.Count}";
            }
        }
    }
}