﻿using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionItemView : ButtonWithActiveBorder
    {
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;

        public Action<ProductionItemView> OnClickEvent { get; set; }
        public ICraftableItem GroupData { get; private set; }

        public void SetData(ICraftableItem groupData)
        {
            GroupData = groupData;

            icon.sprite = _assetsService.GetSprite(groupData.SpriteAssetId);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}