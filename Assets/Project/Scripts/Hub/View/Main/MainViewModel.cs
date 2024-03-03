using Syndicate.Core.View;

namespace Syndicate.Hub.View.Main
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewType CurrentTabType { get; set; }

        public ProductionSectionView ProductionSection { get; set; }
    }
}