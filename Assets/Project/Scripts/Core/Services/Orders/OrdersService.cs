using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class OrdersService : IOrdersService
    {
        [Inject] private readonly IOrdersConfigurationProvider _ordersConfigurationProvider;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IApiService _apiService;
        [Inject] private readonly IProductsService _productsService;

        private readonly Dictionary<CompanyId, TradeOrderObject> _orders = new();

        private bool _isNeedSendData;

        public async UniTask LoadData(TradeState data)
        {
            foreach (var (key, value) in data.Orders)
            {
                _orders.Add(key, value);
            }

            await RefreshAvailableOrders();
        }

        public bool IsCompanyUnlocked(CompanyId companyId) => _orders.Any(x => x.Key == companyId);
        public int GetCompanySize(CompanyId companyId) => _orders[companyId].Size;
        public long GetCompanyRefreshTime(CompanyId companyId) => _orders[companyId].RefreshTime;

        public async UniTask RefreshAvailableOrders()
        {
            foreach (var (key, value) in _orders)
            {
                var isNeedRefresh = DateUtil.GetTime(value.RefreshTime) == 0;
                if (!isNeedRefresh) continue;

                var orders = value.List.Values.ToList();
                for (var i = 0; i < value.Size; i++)
                {
                    if (key == CompanyId.Personal)
                    {
                        var order = orders.FirstOrDefault(x => x.Index == i);
                        if (order == null)
                        {
                            TryCreateOrder(key, i);
                        }

                        continue;
                    }

                    TryCreateOrder(key, i);
                }

                var config = _ordersConfigurationProvider.GetOrderGroupUpgradeData(key);
                _orders[key].RefreshTime = DateUtil.IntervalHourTime(config.RefreshTime).ToFileTime();

                _isNeedSendData = true;
            }

            if (_isNeedSendData)
            {
                await _apiService.Request(_apiService.SetOrders(_orders), Finish);

                void Finish() => _isNeedSendData = false;
            }
        }

        public void TryCreateOrder(CompanyId companyId, int index)
        {
            var orderList = _orders[companyId].List.Values;
            var orderData = orderList.FirstOrDefault(x => x.Index == index);
            if (orderData != null) return;

            var randomItemData = _productsService.GetRandomProduct();
            var partData = new PartObject
            {
                Type = randomItemData.Type,
                Key = randomItemData.Key,
                Count = 1
            };

            var order = new OrderObject
            {
                Guid = Guid.NewGuid(),
                CompanyId = companyId,
                Items = new List<PartObject> { partData },
                Index = index
            };

            _orders[companyId].List.Add(order.Guid, order);
        }

        public OrderObject GetOrder(CompanyId companyId, int index)
        {
            var orders = _orders[companyId].List.Values.ToList();
            return orders.FirstOrDefault(x => x.Index == index);
        }

        public async UniTask CompleteOrder(OrderObject orderData)
        {
            var order = _orders[orderData.CompanyId].List[orderData.Guid];

            var cost = 0;
            foreach (var part in order.Items)
            {
                var item = _productsService.GetProduct(part);
                cost += item.CraftCost;
            }

            var currentCash = _gameService.Cash += cost;
            await _apiService.Request(_apiService.CompleteOrder(order, currentCash), Finish);

            void Finish()
            {
                _orders[orderData.CompanyId].List.Remove(orderData.Guid);

                _gameService.Cash = currentCash;
            }
        }

        public async UniTask AddOrderCompany(CompanyId companyId, int price)
        {
            var currentCash = _gameService.Cash -= price;
            await _apiService.Request(_apiService.SetOrderSize(companyId, 1, currentCash), Finish);

            void Finish()
            {
                var orderGroup = new TradeOrderObject { Size = 1 };
                var config = _ordersConfigurationProvider.GetOrderGroupUpgradeData(companyId);
                orderGroup.RefreshTime = DateUtil.IntervalHourTime(config.RefreshTime).ToFileTime();

                _orders.Add(companyId, orderGroup);

                _gameService.Cash = currentCash;
            }
        }

        public async UniTask AddOrderCompanySize(CompanyId companyId, int price)
        {
            var currentCash = _gameService.Cash -= price;
            var orderSize = _orders[companyId].Size;
            await _apiService.Request(_apiService.SetOrderSize(companyId, orderSize + 1, currentCash), Finish);

            void Finish()
            {
                _orders[companyId].Size++;
                _gameService.Cash = currentCash;
            }
        }
    }
}