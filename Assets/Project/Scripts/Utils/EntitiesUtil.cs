#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using UnityEditor;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class EntitiesUtil
    {
        public static string[] GetSpriteAssetValues()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(SpriteSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var config = AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path);
            var allValues = new List<string>();
            allValues.AddRange(config.Raw.Select(x => x.Id.ToString()));
            allValues.AddRange(config.Weapon.Select(x => x.Id.ToString()));
            allValues.AddRange(config.Armor.Select(x => x.Id.ToString()));
            allValues.AddRange(config.Units.Select(x => x.Id.ToString()));
            return allValues.ToArray();
        }

        public static string[] GetRawItemKeys()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(RawSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<RawSetScriptable>(path).Items;
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }

        public static string[] GetComponentItemKeys()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ProductSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ProductSetScriptable>(path).Items
                .Where(x => x.Type == ItemType.Component);
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }

        public static string[] GetProductItemKeys()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ProductSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ProductSetScriptable>(path).Items
                .Where(x => x.Type == ItemType.Product);
            return allValues.Select(x => x.Key.ToString()).ToArray();
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

        public static string[] GetUnitTypeValues()
        {
            return typeof(UnitTypeId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();
        }

        public static string[] GetUnitValues()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(UnitSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<UnitSetScriptable>(path).Items;
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }
    }
}
#endif