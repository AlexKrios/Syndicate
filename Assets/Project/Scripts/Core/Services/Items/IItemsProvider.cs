using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsProvider
    {
        void LoadItemsData(ItemDto item);

        ItemBaseObject GetItemByKey(string id);
        ICraftableItem GetCraftableItemByKey(string id);

        List<ItemBaseObject> RemoveItems(ICraftableItem data);
    }
}