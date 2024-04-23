using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Services;
using Syndicate.Utils;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionQueueSectionView : MonoBehaviour
    {
        //[Inject] private readonly ConfigurationsScriptable _configurationsScriptable;
        [Inject] private readonly IProductionService _productionService;

        [SerializeField] private List<ProductionQueueCellView> items;

        private async void OnEnable()
        {
            //var productionData = _configurationsScriptable.ProductionSet;
            /*for (var i = 0; i < productionData.Count; i++)
            {
                if (items.ElementAtOrDefault(i) == null) continue;

                items[i].SetQueueUnlockData(productionData[i]);
            }*/

            await UniTask.Delay(100);
            RefreshQueue();
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
            }
        }
    }
}