﻿using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionPartView : MonoBehaviour
    {
        private const float NullAlpha = 0.1f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        [SerializeField] private Image star;

        public void SetData(ItemBaseObject itemBase, int needCount = 0)
        {
            canvasGroup.alpha = itemBase != null ? NotNullAlpha : NullAlpha;
            icon.gameObject.SetActive(itemBase != null);
            count.gameObject.SetActive(itemBase != null);
            star.gameObject.SetActive(itemBase != null);

            if (itemBase != null)
            {
                icon.sprite = _assetsService.GetSprite(itemBase.SpriteAssetId);

                var starCount = ItemsUtil.ParseItemKeyToStar(itemBase.Key);
                star.sprite = _assetsService.GetStarSprite(starCount);
                count.text = needCount.ToString();
            }
        }
    }
}