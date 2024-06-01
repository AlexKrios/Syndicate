using Syndicate.Core.Entities;
using Syndicate.Core.View;

namespace Syndicate.Hub.View.Main
{
    public class UnitSelectionViewModel : ViewModelBase, IPopupViewModel
    {
        public UnitObject CurrentUnit { get; set; }
        public ProductGroupId CurrentProductGroup { get; set; }

        public ProductObject CurrentProduct { get; set; }
    }
}