using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsService
    {
        GroupData GetGroupData(ItemBaseObject itemBase);

        ItemData TryAddItem(ItemBaseObject itemBase);
        List<ItemData> GetAllItems();
        ItemData GetItemData(string id);

        Dictionary<string, object> RemoveItems(ICraftableItem data);
    }
}