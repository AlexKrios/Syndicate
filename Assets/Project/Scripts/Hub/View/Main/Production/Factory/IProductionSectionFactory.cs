using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public interface IProductionSectionFactory
    {
        ProductionProductView CreateProduct(Transform parent);
    }
}