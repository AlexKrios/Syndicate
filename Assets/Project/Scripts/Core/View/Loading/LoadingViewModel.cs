using UniRx;

namespace Syndicate.Core.View
{
    public class LoadingViewModel : ViewModelBase, IPopupViewModel
    {
        public IReactiveProperty<string> LoadingText { get; } = new StringReactiveProperty();
        public IReactiveProperty<float> LoadingPercent { get; } = new FloatReactiveProperty();
    }
}