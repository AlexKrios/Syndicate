using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsProvider
    {
        ItemBaseObject GetItemById(string id);

        ICraftableItem GetCraftableItemById(string id);
    }
}