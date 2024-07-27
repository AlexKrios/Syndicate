using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IOrdersService
    {
        UniTask LoadData(TradeState data);

        UniTask RefreshAvailableOrders();

        bool IsCompanyUnlocked(CompanyId companyId);
        int GetCompanySize(CompanyId companyId);
        long GetCompanyRefreshTime(CompanyId companyId);

        UniTask AddOrderCompany(CompanyId companyId, int price);
        UniTask AddOrderCompanySize(CompanyId companyId, int price);

        OrderObject GetOrder(CompanyId companyId, int index);

        UniTask CompleteOrder(OrderObject orderData);
    }
}