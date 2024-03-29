#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(SpriteAssetId))]
    public class SpriteAssetIdDrawer : PropertyDrawer
    {
        private const string SpriteAssetLabel = "Sprite Asset Id";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var assetId = AssetDatabase.FindAssets($"t:{nameof(SpriteSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = new List<SpriteAssetScriptable>();
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Raw);
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Weapon);
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Armor);
            allValues.AddRange(AssetDatabase.LoadAssetAtPath<SpriteSetScriptable>(path).Units);
            var stringValues = allValues.Select(x => x.Id.ToString()).ToArray();

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var index = Mathf.Max(0, Array.IndexOf(stringValues, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, SpriteAssetLabel, index, stringValues);
            valueProperty.stringValue = stringValues[index];

            EditorGUI.EndProperty();
        }
    }
}
#endif