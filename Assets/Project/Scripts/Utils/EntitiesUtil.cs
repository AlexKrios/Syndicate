using System.Linq;
using System.Reflection;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using UnityEditor;

namespace Syndicate.Utils
{
    public class EntitiesUtil
    {
        public static string[] GetItemValues()
        {
            return typeof(ItemTypeId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();
        }

        public static string[] GetRawValues()
        {
            return typeof(RawId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();
        }

        public static string[] GetComponentValues()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ComponentSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ComponentSetScriptable>(path).Items;
            return allValues.Select(x => x.Id.ToString()).ToArray();
        }

        public static string[] GetProductValues()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ProductSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ProductSetScriptable>(path).Items;
            return allValues.Select(x => x.Id.ToString()).ToArray();
        }

        public static string[] GetProductGroupValues()
        {
            return typeof(ProductGroupId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();
        }

        public static string[] GetSpecificationValues()
        {
            return typeof(SpecificationId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();
        }
    }
}