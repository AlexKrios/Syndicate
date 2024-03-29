using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ItemsProvider : IItemsProvider
    {
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IComponentsService _componentsService;
        [Inject] private readonly IProductsService _productsService;

        public ItemBaseObject GetItemById(string id)
        {
            var itemType = ItemsUtil.GetItemTypeById(id);
            return itemType switch
            {
                ItemType.Raw => _rawService.GetRawById((RawItemId)id),
                ItemType.Component => _componentsService.GetComponentById((ComponentId)id),
                ItemType.Product => _productsService.GetProductById((ProductId)id),
                _ => null
            };
        }

        public T GetItemById<T>(string id) where T : ItemBaseObject
        {
            if (typeof(T) == typeof(RawObject))
                return _rawService.GetRawByKey((RawItemId)id) as T;

            if (typeof(T) == typeof(ComponentObject))
                return _componentsService.GetComponentByKey((ComponentId)id) as T;

            if (typeof(T) == typeof(ProductObject))
                return _productsService.GetProductByKey((ProductId)id) as T;

            return null;
        }

        public ICraftableItem GetCraftableItemById(string id)
        {
            var itemType = ItemsUtil.GetItemTypeById(id);
            return itemType switch
            {
                ItemType.Component => _componentsService.GetComponentById((ComponentId)id),
                ItemType.Product => _productsService.GetProductById((ProductId)id),
                _ => null
            };
        }
    }
}