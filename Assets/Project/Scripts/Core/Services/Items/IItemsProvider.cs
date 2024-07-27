using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsProvider
    {
        void LoadItemsData(ItemDto item);

        ItemBaseObject GetItem(PartObject part);
        ItemBaseObject GetItem(ItemType type, string key);
        ItemBaseObject GetRandomCraftable();

        bool IsHaveNeedItems(List<PartObject> parts);

        List<ItemBaseObject> RemoveItems(ICraftableItem data);
    }
}