using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Syndicate.Core.Signals;
using Unity.VisualScripting;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class GameService : IGameService
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IApiService _apiService;
        [Inject] private readonly IExperienceService _experienceService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IUnitsService _unitsService;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly IExpeditionService _expeditionService;
        [Inject] private readonly IOrdersService _ordersService;

        private string _name;
        public string Name { get; set; }

        private int _cash;
        public int Cash
        {
            get => _cash;
            set
            {
                _cash = value;
                _signalBus.Fire<CashChangeSignal>();
            }
        }

        private int _diamond;
        public int Diamond
        {
            get => _diamond;
            set
            {
                _diamond = value;
                _signalBus.Fire<DiamondChangeSignal>();
            }
        }

        public async UniTask LoadPlayerProfile()
        {
            var data = await _apiService.GetPlayerProfile();
            if (data == null)
            {
                data = new PlayerState();

                var rawDtoList = _rawService.CreateRaw();
                data.Inventory.Items.AddRange(rawDtoList);

                var unitDto = new UnitObject(_configurations.UnitSet.Items[0]).ToDto();
                //unitDto.Outfit.Add(ProductGroupId.Weapon, "trooper_weapon_product");
                data.Units.Roster.Add(unitDto.Key, unitDto);

                var tradeOrderState = data.Trade.Orders;
                tradeOrderState.Add(CompanyId.Personal, new TradeOrderObject());
                tradeOrderState[CompanyId.Personal].Size = 1;
                tradeOrderState.Add(CompanyId.Company1, new TradeOrderObject());
                tradeOrderState[CompanyId.Company1].Size = 1;

                await _apiService.SetStartPlayerProfile(data);
            }

            Name = data.Profile.Name;
            Cash = data.Inventory.Cash;
            Diamond = data.Inventory.Diamond;

            _experienceService.LoadData(data.Profile);

            foreach (var (_, value) in data.Inventory.Items)
            {
                _itemsProvider.LoadItemsData(value);
            }
            _unitsService.LoadUnits(data.Units);

            _productionService.LoadData(data.Production);
            _expeditionService.LoadData(data.Expedition);

            await _ordersService.LoadData(data.Trade);
        }

        public async UniTask SetName(string name)
        {
            await _apiService.Request(_apiService.SetPlayerName(name), Finish);

            void Finish() => Name = name;
        }

        public async UniTask SetCash(int cash)
        {
            await _apiService.Request(_apiService.SetCashCount(cash), Finish);

            void Finish() => Cash = cash;
        }

        public async UniTask SetDiamond(int diamond)
        {
            await _apiService.Request(_apiService.SetDiamondCount(diamond), Finish);

            void Finish() => Diamond = diamond;
        }
    }
}