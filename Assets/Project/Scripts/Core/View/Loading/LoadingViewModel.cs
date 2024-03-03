using UniRx;

namespace Syndicate.Core.View
{
    public class LoadingViewModel : ViewModelBase
    {
        public IReactiveProperty<float> LoadingPercent { get; } = new FloatReactiveProperty();
    }
}