using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductionService
    {
        int Size { get; }

        bool IsHaveNeedItems(ICraftableItem data);

        bool IsHaveFreeCell();

        UniTask RemoveItems(ICraftableItem data);

        int GetFreeCell();

        List<ProductionObject> GetAllProduction();

        void AddProduction(ProductionObject productionObject);

        UniTask RemoveProduction(Guid id);

        void AddProductionSize();

        void AddProductionLevel();
    }
}