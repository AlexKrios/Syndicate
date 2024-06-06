using System;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class UnitItemView : ButtonWithActiveBorder
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Image background;
        [SerializeField] private Image icon;

        public Action<UnitItemView> OnClickEvent { get; set; }
        public UnitObject Data { get; private set; }

        public void SetData(UnitObject data)
        {
            Data = data;

            background.color = _configurations.GetUnitTypeData(Data.UnitTypeId).BgColor;
            icon.sprite = _assetsService.GetSprite(data.IconId);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}