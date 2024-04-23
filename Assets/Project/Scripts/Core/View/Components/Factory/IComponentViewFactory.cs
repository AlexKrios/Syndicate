using UnityEngine;

namespace Syndicate.Core.View
{
    public interface IComponentViewFactory
    {
        T Create<T>(Transform parent) where T : IComponentView;
    }
}