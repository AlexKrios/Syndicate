using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionLocationView : ButtonWithActiveBorder
    {
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image icon;

        public Action<ExpeditionLocationView> OnClickEvent { get; set; }
        public LocationObject Data { get; private set; }

        public void SetData(LocationObject data)
        {
            Data = data;

            icon.sprite = _assetsService.GetSprite(data.IconAssetId);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}