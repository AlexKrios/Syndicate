using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.View;

namespace Syndicate.Hub.View
{
    [UsedImplicitly]
    public class OrderCompanyUpgradeViewModel : ViewModelBase, IPopupViewModel
    {
        public OrderGroupUpgradeScriptable UpgradeData { get; set; }
        public CompanyId CompanyId { get; set; }
    }
}