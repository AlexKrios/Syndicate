using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    public class ItemsProvider : IItemsProvider
    {
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IComponentsService _componentsService;
        [Inject] private readonly IProductsService _productsService;

        public ItemObject GetItem(ItemTypeId itemTypeId, string itemId)
        {
            if (itemTypeId == ItemTypeId.Raw)
                return _rawService.GetRaw((RawId)itemId);

            if (itemTypeId == ItemTypeId.Component)
                return _componentsService.GetComponent((ComponentId)itemId);

            if (itemTypeId == ItemTypeId.Product)
                return _productsService.GetProduct((ProductId)itemId);

            return null;
        }

        public T GetItem<T>(string itemId) where T : ItemObject
        {
            if (typeof(T) == typeof(RawObject))
                return _rawService.GetRaw((RawId)itemId) as T;

            if (typeof(T) == typeof(ComponentObject))
                return _componentsService.GetComponent((ComponentId)itemId) as T;

            if (typeof(T) == typeof(ProductObject))
                return _productsService.GetProduct((ProductId)itemId) as T;

            return null;
        }
    }
}