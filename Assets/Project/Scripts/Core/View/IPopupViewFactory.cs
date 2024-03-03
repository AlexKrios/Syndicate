namespace Syndicate.Core.View
{
    public interface IPopupViewFactory
    {
        ViewBase<T> Build<T>() where T : ViewModelBase;
    }
}