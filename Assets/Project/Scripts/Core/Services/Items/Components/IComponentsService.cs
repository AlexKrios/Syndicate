using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IComponentsService
    {
        ComponentObject GetComponent(ComponentId assetId);

        List<ComponentObject> GetAllProducts();
    }
}