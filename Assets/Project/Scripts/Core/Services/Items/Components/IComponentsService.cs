using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IComponentsService
    {
        List<ComponentObject> GetAllProducts();
        ComponentObject GetComponentByKey(ComponentId key);
        ComponentObject GetComponentById(string id);
    }
}