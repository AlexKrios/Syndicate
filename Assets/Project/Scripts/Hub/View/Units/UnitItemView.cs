using System;
using System.Collections.Generic;
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
        [SerializeField] private List<Image> stars;

        public Action<UnitItemView> OnClickEvent { get; set; }
        public UnitObject Data { get; private set; }

        public void SetData(UnitObject data)
        {
            Data = data;

            background.color = _configurations.GetUnitTypeData(Data.UnitTypeId).BgColor;
            icon.sprite = _assetsService.GetSprite(data.IconId);
            for (var i = 0; i < stars.Count; i++)
            {
                stars[i].gameObject.SetActive(i + 1 <= data.Star);
                stars[i].color = background.color;
            }
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}