using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Profile;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Utils;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionQueueSectionView : MonoBehaviour
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly IComponentViewFactory _componentViewFactory;

        [SerializeField] private List<ProductionQueueCellView> items;
        [SerializeField] private Transform plusTransform;

        private PlayerState PlayerState => _gameService.GetPlayerState();

        private async void OnEnable()
        {
            await UniTask.Delay(100);
            RefreshQueue();
        }

        private void RefreshQueue()
        {
            var queueList = _productionService.GetAllProduction().ToList();
            for (var i = 0; i < PlayerState.Production.Size; i++)
            {
                if (items.ElementAtOrDefault(i) == null)
                {
                    items.Add(_componentViewFactory.Create<ProductionQueueCellView>(transform));
                }

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

            plusTransform.SetAsLastSibling();
        }
    }
}