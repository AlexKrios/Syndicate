using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class OrdersView : PopupViewBase<OrdersViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IOrdersConfigurationProvider _ordersConfigurationProvider;
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IOrdersService _ordersService;

        [SerializeField] private Button close;
        [SerializeField] private TMP_Text timer;
        [SerializeField] private List<OrderTabView> tabs;

        [Header("Personal")]
        [SerializeField] private GameObject personalWrapper;
        [SerializeField] private List<OrderCellView> personalItems;

        [Header("Company")]
        [SerializeField] private GameObject companyWrapper;
        [SerializeField] private List<OrderCompanyTabView> companyTabs;
        [SerializeField] private List<OrderCellView> companyItems;

        private Coroutine _refreshTimer;

        private OrderTabView _currentTab;
        private OrderTabView CurrentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab != null)
                    _currentTab.SetInactive();

                _currentTab = value;
                _currentTab.SetActive();
            }
        }

        private OrderCompanyTabView _currentCompanyTab;
        private OrderCompanyTabView CurrentCompanyTab
        {
            get => _currentCompanyTab;
            set
            {
                if (_currentCompanyTab != null)
                    _currentCompanyTab.SetInactive();

                _currentCompanyTab = value;
                _currentCompanyTab.SetActive();
            }
        }

        private void Awake()
        {
            close.onClick.AddListener(Close);

            _signalBus.Subscribe<OrdersChangeSignal>(() =>
            {
                SetCompanyTabsData();
                SetOrdersData();
            });

            tabs.ForEach(x => x.OnClickEvent += OnTabClick);
            companyTabs.ForEach(x => x.OnClickEvent += OnCompanyTabClick);
        }

        protected override async void OnEnable()
        {
            CurrentTab = tabs.First();

            await _ordersService.RefreshAvailableOrders();

            SetCompanyTabsData();
            SetOrdersData();
            _refreshTimer = StartCoroutine(StartRefreshTimer());

            base.OnEnable();
        }

        private IEnumerator StartRefreshTimer()
        {
            if (_refreshTimer != null)
                StopCoroutine(_refreshTimer);

            timer.gameObject.SetActive(_currentTab.Type == OrderGroupType.Company);
            var companyId = _currentTab.Type == OrderGroupType.Personal ? CompanyId.Personal : _currentCompanyTab.Type;

            while (true)
            {
                var timeEnd = DateTime.FromFileTime(_ordersService.GetCompanyRefreshTime(companyId));
                var needTime = timeEnd - DateTime.Now;
                timer.text = new DateTime(needTime.Ticks).ToString(Constants.TimerTemplate);

                yield return new WaitForSeconds(1);

                if (needTime.TotalSeconds <= 0)
                {
                    yield return _ordersService.RefreshAvailableOrders();
                    _refreshTimer = StartCoroutine(StartRefreshTimer());

                    _signalBus.Fire(new OrdersChangeSignal());
                }
            }

            // ReSharper disable once IteratorNeverReturns
        }

        private void OnTabClick(OrderTabView tab)
        {
            if (CurrentTab == tab) return;

            CurrentTab = tab;

            personalWrapper.SetActive(tab.Type == OrderGroupType.Personal);
            companyWrapper.SetActive(tab.Type == OrderGroupType.Company);

            if (tab.Type == OrderGroupType.Company)
            {
                CurrentCompanyTab = companyTabs.First();
            }

            SetOrdersData();
            _refreshTimer = StartCoroutine(StartRefreshTimer());
        }

        private void SetCompanyTabsData()
        {
            var isBuyAvailable = true;
            foreach (var tab in companyTabs)
            {
                if (_ordersService.IsCompanyUnlocked(tab.Type))
                {
                    var iconKey = _configurations.CompanySet.First(x => x.Key == tab.Type).IconAssetId;
                    tab.SetStateActive(_assetsService.GetSprite(iconKey));
                }
                else
                {
                    var config = _ordersConfigurationProvider.GetOrderGroupUpgradeData(tab.Type);
                    tab.SetStateBuy(config, isBuyAvailable);

                    isBuyAvailable = false;
                }
            }
        }

        private void OnCompanyTabClick(OrderCompanyTabView tab)
        {
            if (CurrentCompanyTab == tab) return;

            CurrentCompanyTab = tab;

            SetOrdersData();
            _refreshTimer = StartCoroutine(StartRefreshTimer());
        }

        private void SetOrdersData()
        {
            var cells = _currentTab.Type == OrderGroupType.Personal ? personalItems : companyItems;
            var companyId = _currentTab.Type == OrderGroupType.Personal ? CompanyId.Personal : _currentCompanyTab.Type;

            personalWrapper.SetActive(_currentTab.Type == OrderGroupType.Personal);
            companyWrapper.SetActive(_currentTab.Type == OrderGroupType.Company);

            SetOrdersData(cells, companyId);
        }

        private void SetOrdersData(IReadOnlyList<OrderCellView> cells, CompanyId companyId)
        {
            for (var i = 0; i < cells.Count; i++)
            {
                var companySize = _ordersService.GetCompanySize(companyId);
                if (companySize >= i + 1)
                {
                    var order = _ordersService.GetOrder(companyId, i);
                    if (order != null)
                    {
                        cells[i].SetActiveState(order);
                    }
                    else
                    {
                        var timeEnd = DateTime.FromFileTime(_ordersService.GetCompanyRefreshTime(companyId));
                        cells[i].SetEmptyState(timeEnd);
                    }
                }
                else
                {
                    var config = _ordersConfigurationProvider.GetOrderUpgradeData(companyId, i + 1);
                    cells[i].SetType(companyId);
                    cells[i].SetUpgradeState(config, companySize == i);
                }
            }
        }
    }
}