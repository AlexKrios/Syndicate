namespace Syndicate.Core.Signals
{
    public class LevelChangeSignal
    {
        public int Level { get; }

        public LevelChangeSignal(int level)
        {
            Level = level;
        }
    }
}