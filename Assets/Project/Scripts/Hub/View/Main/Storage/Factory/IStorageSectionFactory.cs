using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public interface IStorageSectionFactory
    {
        StorageItemView CreateItem(Transform parent);
    }
}