using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IProductionService
    {
        int Size { get; }

        void LoadData(ProductionState state);

        bool IsHaveNeedItems(ICraftableItem data);
        bool IsHaveFreeCell();
        int GetFreeCell();

        List<ProductionObject> GetAllProduction();
        UniTask AddProduction(ProductionObject productionObject);
        void CompleteProduction(Guid id, ICraftableItem item);

        void AddProductionSize();
        void AddProductionLevel();
    }
}