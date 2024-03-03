namespace Syndicate.Core.View
{
    public interface IPopupService
    {
        T Show<T>() where T : ViewModelBase;

        T Get<T>(bool onlyModel = false) where T : ViewModelBase;
    }
}