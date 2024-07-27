using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.View;
using Syndicate.Utils;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ProductionQueueSectionView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;

        [SerializeField] private List<ProductionQueueCellView> items;
        [SerializeField] private Transform plusTransform;

        private void Awake()
        {
            _signalBus.Subscribe<ProductionSizeChangeSignal>(RefreshQueue);
        }

        public void RefreshQueue()
        {
            var queueList = _productionService.GetAllProduction().ToList();
            for (var i = 0; i < _productionService.Size; i++)
            {
                if (items.ElementAtOrDefault(i) == null)
                    items.Add(_componentViewFactory.Create<ProductionQueueCellView>(transform));

                if (queueList.ElementAtOrDefault(i) != null && queueList[i].Index == i + 1)
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
            }

            if (_productionService.IsMaxSize)
                plusTransform.gameObject.SetActive(false);
            else
                plusTransform.SetAsLastSibling();
        }
    }
}