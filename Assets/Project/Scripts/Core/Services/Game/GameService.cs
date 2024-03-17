using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class GameService : IGameService
    {
        [Inject] private readonly IApiService _apiService;

        private PlayerProfile _userProfile;

        public async UniTask CreateGame()
        {
            var data = await _apiService.GetPlayerProfile();
            if (data != null)
            {
                _userProfile = data;
            }
            else
            {
                _userProfile = new PlayerProfile();
                await _apiService.SetStartPlayerProfile(_userProfile);
            }
        }

        public PlayerProfile GetPlayerProfile() => _userProfile;
    }
}