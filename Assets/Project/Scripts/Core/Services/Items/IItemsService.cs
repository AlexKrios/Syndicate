using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsService
    {
        GroupData GetGroupData(ItemBaseObject itemBase);

        ItemData TryAddItem(ItemBaseObject itemBase);
        List<ItemData> GetAllItems();
        ItemData GetItemData(ItemBaseObject item);
        ItemData GetItemData(ItemType itemType, string key);

        Dictionary<string, object> RemoveItems(ICraftableItem data);
    }
}