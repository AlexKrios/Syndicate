using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public class GameService : IGameService
    {
        private PlayerProfile _userProfile;

        public GameService()
        {
            CreateGame();
        }

        public void CreateGame()
        {
            _userProfile = new PlayerProfile();
        }

        public PlayerProfile GetPlayerProfile() => _userProfile;
    }
}