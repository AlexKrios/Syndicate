namespace Syndicate.Core.Profile
{
    public class ExperienceState
    {
        public int Experience { get; set; }
        public int Level { get; set; }

        public ExperienceState()
        {
            Experience = 0;
            Level = 1;
        }
    }
}