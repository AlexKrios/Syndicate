using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class UnitItemView : ButtonWithActiveBorder
    {
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;

        public Action<UnitItemView> OnClickEvent { get; set; }
        public ICraftableItem Data { get; private set; }

        public void SetData(ICraftableItem data)
        {
            Data = data;

            icon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}