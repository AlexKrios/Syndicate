using System;
using System.Linq;
using Syndicate.Core.Configurations;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(MusicAssetId))]
    public class MusicAssetIdDrawer : PropertyDrawer
    {
        private const string MusicLabel = "Music Id: ";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var assetId = AssetDatabase.FindAssets($"t:{nameof(MusicSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<MusicSetScriptable>(path).Items;
            var selectedValues = allValues.Select(x => x.Id.ToString()).ToArray();

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var index = Mathf.Max(0, Array.IndexOf(allValues.ToArray(), valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, MusicLabel, index, selectedValues);
            valueProperty.stringValue = selectedValues[index];

            EditorGUI.EndProperty();
        }
    }
}