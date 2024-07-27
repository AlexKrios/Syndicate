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
        [Inject] private readonly IApiService _apiService;

        public int Experience { get; private set; }
        private int Level { get; set; }

        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public void LoadData(ProfileState state)
        {
            Experience = state.Experience;
        }

        public int GetCurrentLevel()
        {
            Level = GetLevelByExperience(Experience);

            return Level;
        }

        public float GetCurrentLevelPercent()
        {
            var previousCap = Level == 1 ? 0 : GetLevelCap(Level - 1);
            var currentExp = Experience - previousCap;
            var needExp = GetLevelCap(Level) - previousCap;

            return (float)currentExp / needExp * 100;
        }

        public async void SetExperience(int experience)
        {
            var newExperience = Experience += experience;
            await _apiService.Request(_apiService.SetExperience(newExperience), Finish);

            void Finish()
            {
                Experience = newExperience;
                _signalBus.Fire(new ExperienceChangeSignal(Experience));

                if (Experience >= GetLevelCap(Level))
                {
                    Level++;
                    _signalBus.Fire(new LevelChangeSignal(Level));
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