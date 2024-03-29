using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class StorageItemView : ButtonWithActiveBorder
    {
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private Image star;

        public Action<StorageItemView> OnClickEvent { get; set; }
        public ItemData ItemData { get; private set; }
        public ICraftableItem GroupData { get; private set; }

        public void SetData(ItemData itemData, ICraftableItem groupData)
        {
            ItemData = itemData;
            GroupData = groupData;

            icon.sprite = _assetsService.GetSprite(groupData.SpriteAssetId);
            count.text = itemData.Count.ToString();

            var starCount = ItemsUtil.ParseItemIdToStar(groupData.Id);
            star.sprite = _assetsService.GetStarSprite(starCount);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}