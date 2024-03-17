using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IItemsService
    {
        GroupData GetGroupData(ItemType itemType, string key);

        ItemData GetItemData(ItemType itemType, string key);
    }
}