using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ItemsProvider : IItemsProvider
    {
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IComponentsService _componentsService;
        [Inject] private readonly IProductsService _productsService;

        public ItemBaseObject GetItem(ItemType itemType, string key)
        {
            return itemType switch
            {
                ItemType.Raw => _rawService.GetRaw((RawItemId)key),
                ItemType.Component => _componentsService.GetComponent((ComponentId)key),
                ItemType.Product => _productsService.GetProduct((ProductId)key),
                _ => null
            };
        }

        public T GetItem<T>(string itemId) where T : ItemBaseObject
        {
            if (typeof(T) == typeof(RawObject))
                return _rawService.GetRaw((RawItemId)itemId) as T;

            if (typeof(T) == typeof(ComponentObject))
                return _componentsService.GetComponent((ComponentId)itemId) as T;

            if (typeof(T) == typeof(ProductObject))
                return _productsService.GetProduct((ProductId)itemId) as T;

            return null;
        }

        public ItemBaseObject GetItemById(ItemType itemType, string id)
        {
            return itemType switch
            {
                ItemType.Raw => _rawService.GetRaw((RawItemId)id),
                ItemType.Component => _componentsService.GetComponent((ComponentId)id),
                ItemType.Product => _productsService.GetProduct((ProductId)id),
                _ => null
            };
        }

        public ICraftableItem GetCraftableItemById(ItemType itemType, string id)
        {
            return itemType switch
            {
                ItemType.Component => _componentsService.GetComponentById((ComponentId)id),
                ItemType.Product => _productsService.GetProductById((ProductId)id),
                _ => null
            };
        }
    }
}