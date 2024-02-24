namespace Syndicate.Core.View
{
    public interface IViewModelFactory
    {
        T Build<T>() where T : ViewModelBase;
    }
}