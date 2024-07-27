using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class OrderCellView : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductsService _productsService;
        [Inject] private readonly IOrdersService _ordersService;

        [Header("Active")]
        [SerializeField] private GameObject activeWrapper;
        [SerializeField] private List<OrderItemView> activeItems;
        [SerializeField] private TMP_Text activeReward;
        [SerializeField] private Button activeAccept;

        [Header("Buy")]
        [SerializeField] private GameObject buyWrapper;
        [SerializeField] private Button buyButton;
        [SerializeField] private TMP_Text buyCost;

        [Header("Empty")]
        [SerializeField] private GameObject emptyWrapper;
        [SerializeField] private TMP_Text emptyTimer;

        private CompanyId _companyId;
        private OrderObject _orderData;
        private OrderUpgradeScriptable _upgradeData;
        private Coroutine _refreshTimer;

        private void Awake()
        {
            activeAccept.onClick.AddListener(OnAcceptClick);
            buyButton.onClick.AddListener(OnBuyClick);
        }

        public void SetType(CompanyId companyId)
        {
            _companyId = companyId;
        }

        public void SetActiveState(OrderObject data)
        {
            _orderData = data;

            for (var i = 0; i < activeItems.Count; i++)
            {
                var partData = data.Items.ElementAtOrDefault(i);
                if (partData != null)
                {
                    var item = _productsService.GetProduct(data.Items[i]);
                    activeItems[i].SetData(item);
                    activeReward.text = string.Format(Constants.CashPattern, item.CraftCost);
                }

                activeItems[i].gameObject.SetActive(partData != null);
            }

            activeAccept.interactable = _itemsProvider.IsHaveNeedItems(data.Items);

            SetState(OrderCellState.Active);
        }

        public void SetEmptyState(DateTime timeEnd)
        {
            _refreshTimer = StartCoroutine(StartEmptyTimer());

            IEnumerator StartEmptyTimer()
            {
                if (_refreshTimer != null)
                    StopCoroutine(_refreshTimer);

                SetState(OrderCellState.Empty);

                while (true)
                {
                    var ticks = (timeEnd - DateTime.Now).Ticks;
                    emptyTimer.text = new DateTime(ticks).ToString(Constants.TimerTemplate);

                    yield return new WaitForSeconds(1);
                }
                // ReSharper disable once IteratorNeverReturns
            }
        }

        public void SetUpgradeState(OrderUpgradeScriptable data, bool isAvailable)
        {
            _upgradeData = data;

            buyButton.interactable = _gameService.Cash >= data.Cost;
            buyCost.text = string.Format(Constants.CashPattern, data.Cost);

            buyButton.gameObject.SetActive(isAvailable);
            buyCost.gameObject.SetActive(isAvailable);

            SetState(OrderCellState.Buy);
        }

        private void SetState(OrderCellState state)
        {
            activeWrapper.SetActive(state == OrderCellState.Active);
            buyWrapper.SetActive(state == OrderCellState.Buy);
            emptyWrapper.SetActive(state == OrderCellState.Empty);
        }

        private async void OnAcceptClick()
        {
            await _ordersService.CompleteOrder(_orderData);

            _signalBus.Fire(new OrdersChangeSignal());
        }

        private async void OnBuyClick()
        {
            await _ordersService.AddOrderCompanySize(_companyId, _upgradeData.Cost);

            _signalBus.Fire(new OrdersChangeSignal());
        }
    }
}