using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductionService
    {
        void AddProduction(ProductionObject productionObject);

        List<ProductionObject> GetAllProduction();

        bool IsHaveFreeCell();
    }
}