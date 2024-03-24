using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class GameService : IGameService
    {
        [Inject] private readonly IApiService _apiService;
        [Inject] private readonly IRawService _rawService;

        private PlayerProfile _playerProfile;

        public PlayerProfile GetPlayerProfile() => _playerProfile;

        public void CreatePlayerProfile()
        {
            _playerProfile = new PlayerProfile();
        }

        public async UniTask LoadPlayerProfile()
        {
            var data = await _apiService.GetPlayerProfile();
            if (data != null)
            {
                _playerProfile = data;
            }
            else
            {
                foreach (var raw in _rawService.GetAllRaw())
                {
                    var itemData = new ItemData { ItemType = ItemType.Raw, Id = raw.Id, Count = 50 };
                    _playerProfile.Inventory.ItemsData.Add(raw.Id, itemData);
                }

                await _apiService.SetStartPlayerProfile(_playerProfile);
            }
        }
    }
}