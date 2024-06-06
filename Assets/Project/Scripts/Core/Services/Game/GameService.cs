using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class GameService : IGameService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IApiService _apiService;
        [Inject] private readonly IExperienceService _experienceService;
        [Inject] private readonly IUnitsService _unitsService;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly IExpeditionService _expeditionService;

        private PlayerState _playerState;

        public PlayerState GetPlayerState() => _playerState;

        public void CreatePlayerProfile()
        {
            _playerState = new PlayerState();
        }

        public async UniTask LoadPlayerProfile()
        {
            var data = await _apiService.GetPlayerProfile();
            if (data != null)
            {
                _playerState = data;
            }
            else
            {
                foreach (var raw in _rawService.GetAllRaw())
                {
                    raw.Count = 50;
                    _playerState.Inventory.ItemsData.Add(raw.Key, raw.ToDto());
                }

                var unitDto = new UnitObject(_configurations.UnitSet.Items[0]).ToDto();
                unitDto.Outfit.Add(ProductGroupId.Weapon, "pro|trooper_weapon_product|2");
                _playerState.Units.Roster.Add(unitDto.Key, unitDto);

                await _apiService.SetStartPlayerProfile(_playerState);
            }

            _experienceService.LoadData(_playerState.Profile);

            foreach (var (_, value) in _playerState.Inventory.ItemsData)
            {
                _itemsProvider.LoadItemsData(value);
            }
            _unitsService.LoadUnits(_playerState.Units);

            _productionService.LoadData(_playerState.Production);
            _expeditionService.LoadData(_playerState.Expedition);
        }
    }
}