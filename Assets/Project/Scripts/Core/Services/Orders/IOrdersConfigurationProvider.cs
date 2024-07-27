using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IOrdersConfigurationProvider
    {
        OrderGroupUpgradeScriptable GetOrderGroupUpgradeData(CompanyId companyId);

        OrderUpgradeScriptable GetOrderUpgradeData(CompanyId companyId, int index);
    }
}