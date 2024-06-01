using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IComponentsService
    {
        void LoadComponentObjectData(ItemDto data);

        List<ComponentObject> GetAllComponents();
        ComponentObject GetComponentByKey(ComponentId key);
    }
}