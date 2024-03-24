using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class StoragePartView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsService _itemsService;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetData(ItemBaseObject itemBase, int needCount = 0)
        {
            icon.gameObject.SetActive(itemBase != null);
            count.gameObject.SetActive(itemBase != null);
            canvasGroup.alpha = itemBase != null ? NotNullAlpha : NullAlpha;

            if (itemBase != null)
            {
                icon.sprite = _assetsService.GetSprite(itemBase.SpriteAssetId);

                var item = _itemsService.GetItemData(itemBase);
                count.text = $"{needCount}/{item.Count}";
            }
        }
    }
}