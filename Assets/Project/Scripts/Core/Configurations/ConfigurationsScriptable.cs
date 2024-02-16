using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "Configurations", menuName = "Scriptable/Configurations", order = -100)]
    public class ConfigurationsScriptable : ScriptableObject
    {
        [Header("Products")]
        [SerializeField] private ProductGroupSetScriptable productGroupSet;

        public ProductGroupSetScriptable ProductGroupSet => productGroupSet;
    }
}