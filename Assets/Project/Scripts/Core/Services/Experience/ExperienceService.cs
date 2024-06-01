using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Profile;
using Syndicate.Core.Signals;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ExperienceService : IExperienceService, IService
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ConfigurationsScriptable _configurationsScriptable;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IApiService _apiService;

        private ProfileState Profile => _gameService.GetPlayerState().Profile;

        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public int GetCurrentLevel()
        {
            Profile.Level = GetLevelByExperience(Profile.Experience);

            return Profile.Level;
        }

        public float GetCurrentLevelPercent(int experience)
        {
            var previousCap = Profile.Level == 1 ? 0 : GetLevelCap(Profile.Level - 1);
            var currentExp = experience - previousCap;
            var needExp = GetLevelCap(Profile.Level) - previousCap;

            return (float)currentExp / needExp * 100;
        }

        public async void SetExperience(int experience)
        {
            var newExperience = Profile.Experience += experience;
            await _apiService.Request(_apiService.SetExperience(newExperience), Finish);

            void Finish()
            {
                Profile.Experience = newExperience;
                _signalBus.Fire(new ExperienceChangeSignal(Profile.Experience));

                if (Profile.Experience >= GetLevelCap(Profile.Level))
                {
                    Profile.Level++;
                    _signalBus.Fire(new LevelChangeSignal(Profile.Level));
                }
            }
        }

        private int GetLevelCap(int level)
        {
            return _configurationsScriptable.ExperienceSet.First(x => x.Level == level).Cap;
        }

        private int GetLevelByExperience(int experience)
        {
            if (experience == 0)
            {
                return 1;
            }

            var expSet = _configurationsScriptable.ExperienceSet;
            var nextLevelIndex = expSet.IndexOf(expSet.First(x => x.Cap > experience));
            return nextLevelIndex == 0 ? 1 : expSet[nextLevelIndex].Level;
        }
    }
}