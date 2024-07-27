using System.Linq;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class OrdersConfigurationProvider : IOrdersConfigurationProvider
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        public OrderGroupUpgradeScriptable GetOrderGroupUpgradeData(CompanyId companyId)
            => _configurations.OrderSet.Upgrade.First(x => x.CompanyId == companyId);

        public OrderUpgradeScriptable GetOrderUpgradeData(CompanyId companyId, int index)
        {
            var groupConfig = GetOrderGroupUpgradeData(companyId);

            return groupConfig.Data.First(x => x.Number == index);
        }
    }
}