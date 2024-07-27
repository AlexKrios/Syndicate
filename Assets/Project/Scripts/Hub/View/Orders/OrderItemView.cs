using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class OrderItemView : MonoBehaviour
    {
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;

        public void SetData(ItemBaseObject item, int itemCount = 0)
        {
            icon.sprite = _assetsService.GetSprite(item.SpriteAssetId);
            count.gameObject.SetActive(itemCount != 0);
            count.text = itemCount.ToString();
        }
    }
}