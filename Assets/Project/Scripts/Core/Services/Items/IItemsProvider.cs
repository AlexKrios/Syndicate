using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsProvider
    {
        ItemBaseObject GetItem(ItemType itemType, string itemId);

        T GetItem<T>(string itemId) where T : ItemBaseObject;

        ItemBaseObject GetItemById(ItemType itemType, string id);

        ICraftableItem GetCraftableItemById(ItemType itemType, string id);
    }
}