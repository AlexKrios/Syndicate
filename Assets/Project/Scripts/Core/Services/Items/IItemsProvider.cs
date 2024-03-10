using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsProvider
    {
        ItemObject GetItem(ItemTypeId itemTypeId, string itemId);

        T GetItem<T>(string itemId) where T : ItemObject;
    }
}