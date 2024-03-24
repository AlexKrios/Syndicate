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
            var allValues = new List<SpriteAssetScriptable>();
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Raw);
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Weapon);
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Armor);
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Units);
            return allValues.Select(x => x.Id.ToString()).ToArray();
        }

        public static string[] GetRawItemKeys()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(RawSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<RawSetScriptable>(path).Items;
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }

        public static string GetRawItemIdsByKey(string key)
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(RawSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<RawSetScriptable>(path).Items;
            return allValues.First(x => x.Key == key).Id;
        }

        public static string[] GetRawGroupValues()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(RawSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<RawSetScriptable>(path).Groups;
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }

        public static string[] GetComponentItemKeys()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ComponentSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ComponentSetScriptable>(path).Items;
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }

        public static string GetComponentItemIdByKey(string key)
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ComponentSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ComponentSetScriptable>(path).Items;
            return allValues.First(x => x.Key == key).Id;
        }

        public static string[] GetProductItemKeys()
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ProductSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ProductSetScriptable>(path).Items;
            return allValues.Select(x => x.Key.ToString()).ToArray();
        }

        public static string GetProductItemIdByKey(string key)
        {
            var assetId = AssetDatabase.FindAssets($"t:{nameof(ProductSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<ProductSetScriptable>(path).Items;
            return allValues.First(x => x.Key == key).Id;
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