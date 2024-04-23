namespace Syndicate.Core.View
{
    public interface IScreenViewFactory
    {
        ViewBase<T> Build<T>() where T : ViewModelBase;
    }
}