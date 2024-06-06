using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Utils;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View
{
    public class ExpeditionQueueSectionView : MonoBehaviour
    {
        [Inject] private readonly IExpeditionService _expeditionService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;

        [SerializeField] private List<ExpeditionQueueCellView> items;
        [SerializeField] private Transform plusTransform;

        public void RefreshQueue()
        {
            var queueList = _expeditionService.GetAllExpedition().ToList();
            for (var i = 0; i < _expeditionService.Size; i++)
            {
                if (items.ElementAtOrDefault(i) == null)
                    items.Add(_componentViewFactory.Create<ExpeditionQueueCellView>(transform));

                if (queueList.ElementAtOrDefault(i) != null)
                {
                    items[i].SetData(queueList[i]);
                    if (DateUtil.GetTime(queueList[i].TimeEnd) > 0)
                        items[i].SetStateBusy();
                    else
                        items[i].SetStateFinish();

                    continue;
                }

                if (_expeditionService.Size >= i + 1)
                    items[i].SetStateReady();
            }

            plusTransform.SetAsLastSibling();
        }
    }
}