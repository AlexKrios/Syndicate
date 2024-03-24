using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Syndicate.Core.Configurations;
using Syndicate.Core.Services;
using Syndicate.Core.Utils;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionQueueSectionView : MonoBehaviour
    {
        [Inject] private readonly ConfigurationsScriptable _configurationsScriptable;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly InputLocker _inputLocker;

        [SerializeField] private Button close;

        [Space]
        [SerializeField] private List<ProductionQueueCellView> items;

        [Space]
        [SerializeField] private CanvasGroup blackoutCanvasGroup;
        [SerializeField] private RectTransform wrapperTransform;
        [SerializeField] private CanvasGroup wrapperCanvasGroup;

        private Sequence _sequence;

        private void Awake()
        {
            close.onClick.AddListener(Close);

            var productionData = _configurationsScriptable.ProductionSet;
            for (var i = 0; i < productionData.Count; i++)
            {
                items[i].SetQueueUnlockData(productionData[i]);
            }
        }

        private void OnEnable()
        {
            _sequence = DOTween.Sequence()
                .PrependCallback(() => _inputLocker.Lock())
                .Join(blackoutCanvasGroup.DOFade(1, 0.25f).From(0))
                .AppendInterval(0.1f)
                .AppendCallback(RefreshQueue)
                .Join(wrapperCanvasGroup.DOFade(1, 0.25f).From(0))
                .Join(wrapperTransform.DOLocalMoveY(50, 0.25f).SetRelative(true))
                .OnComplete(() => _inputLocker.Unlock());
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }

        private void Close()
        {
            _sequence = DOTween.Sequence()
                .PrependCallback(() => _inputLocker.Lock())
                .Join(wrapperCanvasGroup.DOFade(0, 0.25f).From(1))
                .Join(wrapperTransform.DOLocalMoveY(-50, 0.25f).SetRelative(true))
                .AppendInterval(0.1f)
                .Join(blackoutCanvasGroup.DOFade(0, 0.25f).From(1))
                .OnComplete(() =>
                {
                    _inputLocker.Unlock();
                    gameObject.SetActive(false);
                });
        }

        private void RefreshQueue()
        {
            var queueList = _productionService.GetAllProduction().ToList();
            for (var i = 0; i < items.Count; i++)
            {
                if (queueList.ElementAtOrDefault(i) != null)
                {
                    items[i].SetData(queueList[i]);
                    if (DateUtil.GetTime(queueList[i].TimeEnd) > 0)
                        items[i].SetStateBusy();
                    else
                        items[i].SetStateFinish();

                    continue;
                }

                if (_productionService.Size >= i + 1)
                    items[i].SetStateReady();
                else
                    items[i].SetStateLocked();
            }
        }
    }
}