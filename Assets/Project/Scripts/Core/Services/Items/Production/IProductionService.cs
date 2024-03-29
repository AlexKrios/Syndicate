using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductionService
    {
        int Size { get; }

        string GetItemPreset(string productId);

        bool IsHaveNeedItems(ICraftableItem data);
        bool IsHaveFreeCell();
        int GetFreeCell();

        void RecalculateItemParts(ICraftableItem item);

        List<ProductionObject> GetAllProduction();
        void AddProduction(ProductionObject productionObject);
        void CompleteProduction(Guid id, ItemData itemData, GroupData groupData);

        void AddProductionSize();
        void AddProductionLevel();
    }
}