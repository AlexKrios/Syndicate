using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public interface IUnitSectionFactory
    {
        UnitItemView CreateUnit(Transform parent);
    }
}