using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Syndicate.Core.Services;
using Syndicate.Core.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionQueueSectionView : MonoBehaviour
    {
        [Inject] private readonly IGameService _gameService;
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
        }

        private void Start()
        {
            var queueSize = _gameService.GetPlayerProfile().production.queueSize;
            var queueList = _productionService.GetAllProduction().OrderBy(x => x.TimeEnd).ToList();
            for (var i = 0; i < items.Count; i++)
            {
                if (queueList.ElementAtOrDefault(i) != null)
                {
                    items[i].SetData(queueList[i]);
                    items[i].SetStateBusy();
                    continue;
                }

                if (queueSize >= i + 1)
                    items[i].SetStateReady();
                else
                    items[i].SetStateLocked();
            }
        }

        private void OnEnable()
        {
            _sequence = DOTween.Sequence()
                .PrependCallback(() => _inputLocker.Lock())
                .Join(blackoutCanvasGroup.DOFade(1, 0.25f).From(0))
                .AppendInterval(0.1f)
                .Join(wrapperCanvasGroup.DOFade(1, 0.25f).From(0))
                .Join(wrapperTransform.DOLocalMoveY(50, 0.25f))
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
                .Join(wrapperTransform.DOLocalMoveY(-50, 0.25f))
                .AppendInterval(0.1f)
                .Join(blackoutCanvasGroup.DOFade(0, 0.25f).From(1))
                .OnComplete(() =>
                {
                    _inputLocker.Unlock();
                    gameObject.SetActive(false);
                });
        }

        /*public void SetData(ItemObject itemObject)
        {
            switch (itemObject)
            {
                case ComponentObject componentObject:
                    var componentSpecifications = componentObject.Specifications;
                    SetSpecification(componentSpecifications);
                    break;

                case ProductObject productObject:
                    var productSpecifications = _specificationsUtil.GetProductSpecificationValues(productObject);
                    SetSpecification(productSpecifications);
                    break;
            }
        }

        private void SetSpecification(IReadOnlyCollection<SpecificationObject> specificationsList)
        {
            foreach (var specification in specifications)
            {
                var needSpecification = specificationsList.FirstOrDefault(x => x.Type == specification.Id);
                if (needSpecification == null)
                {
                    specification.ResetData();
                    continue;
                }

                specification.SetData(needSpecification);
            }
        }*/
    }
}