using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ExperienceService : IExperienceService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurationsScriptable;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IApiService _apiService;

        public Action OnExperienceSet { get; set; }
        public Action OnLevelSet { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();

        public UniTask Initialize()
        {
            Experience = PlayerProfile.Inventory.Experience;
            Level = GetLevelByExperience(Experience);

            OnExperienceSet?.Invoke();
            OnLevelSet?.Invoke();

            return UniTask.CompletedTask;
        }

        public async void SetExperience(int experience)
        {
            PlayerProfile.Inventory.Experience += experience;
            if (Experience >= GetCurrentLevelCap())
            {
                Level++;
                OnLevelSet?.Invoke();
            }

            await _apiService.SetExperience(Experience);
            OnExperienceSet?.Invoke();
        }

        private int GetCurrentLevelCap()
        {
            return _configurationsScriptable.ExperienceSet.First(x => x.Level == Level).Cap;
        }

        private int GetLevelByExperience(int experience)
        {
            return _configurationsScriptable.ExperienceSet.First(x => x.Cap > experience).Level;
        }
    }
}