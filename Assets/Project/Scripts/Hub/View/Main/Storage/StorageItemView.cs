using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
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

        public Action<StorageItemView> OnClickEvent { get; set; }
        public ItemData ItemData { get; private set; }
        public ICraftableItem GroupData { get; private set; }

        public void SetData(ItemData itemData, ICraftableItem groupData)
        {
            ItemData = itemData;
            GroupData = groupData;

            icon.sprite = _assetsService.GetSprite(GroupData.SpriteAssetId);
            count.text = itemData.Count.ToString();
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}