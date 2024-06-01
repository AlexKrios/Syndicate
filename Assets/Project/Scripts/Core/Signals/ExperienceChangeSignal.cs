namespace Syndicate.Core.Signals
{
    public class ExperienceChangeSignal
    {
        public int Experience { get; }

        public ExperienceChangeSignal(int experience)
        {
            Experience = experience;
        }
    }
}