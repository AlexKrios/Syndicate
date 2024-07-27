﻿using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class StorageItemView : ButtonWithActiveBorder
    {
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;

        public Action<StorageItemView> OnClickEvent { get; set; }
        public ICraftableItem ItemData { get; private set; }

        public void SetData(ICraftableItem itemData)
        {
            ItemData = itemData;

            icon.sprite = _assetsService.GetSprite(itemData.SpriteAssetId);
            count.text = itemData.Count.ToString();
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}