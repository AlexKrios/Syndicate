using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ItemsService : IItemsService
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IItemsProvider _itemsProvider;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();
        private Dictionary<string, GroupData> Group => PlayerProfile.Inventory.GroupsData;
        private Dictionary<string, ItemData> Items => PlayerProfile.Inventory.ItemsData;

        public GroupData GetGroupData(ItemBaseObject itemBase)
        {
            var id = ItemsUtil.ParseItemToId(itemBase);
            if (!Group.TryGetValue(id, out _))
            {
                var groupData = itemBase.ToGroupData(id);
                Group.Add(id, groupData);
            }

            return Group[id];
        }

        public ItemData TryAddItem(ItemBaseObject itemBase)
        {
            var id = ItemsUtil.ParseItemToId(itemBase);
            if (!Items.TryGetValue(id, out _))
            {
                var itemData = itemBase.ToItemData(id);
                Items.Add(id, itemData);
            }

            return Items[id];
        }

        public List<ItemData> GetAllItems() => Items.Values.ToList();

        public ItemData GetItemData(string id)
        {
            var itemBase = _itemsProvider.GetItemById(id);
            TryAddItem(itemBase);
            return Items[ItemsUtil.ParseItemToId(itemBase)];
        }

        public Dictionary<string, object> RemoveItems(ICraftableItem data)
        {
            var sendList = new Dictionary<string, object>();
            foreach (var part in data.Recipe.Parts)
            {
                var item = GetItemData(part.Id);
                item.Count -= part.Count;

                sendList.Add($"{ApiService.ItemsPath}/{item.Id}/Count", item.Count);
            }

            return sendList;
        }
    }
}