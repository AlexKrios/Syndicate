namespace Syndicate.Core.View
{
    public interface IScreenService
    {
        T Show<T>() where T : ViewModelBase;

        T Get<T>(bool onlyModel = false) where T : ViewModelBase;

        void Back();
    }
}