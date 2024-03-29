#if UNITY_EDITOR
using System;
using System.Linq;
using Syndicate.Core.Configurations;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(AudioAssetId))]
    public class AudioAssetIdDrawer : PropertyDrawer
    {
        private const string AudioLabel = "Audio Id: ";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var assetId = AssetDatabase.FindAssets($"t:{nameof(AudioSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<AudioSetScriptable>(path).Items;
            var stringValues = allValues.Select(x => x.Id.ToString()).ToArray();

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var index = Mathf.Max(0, Array.IndexOf(stringValues, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, AudioLabel, index, stringValues);
            valueProperty.stringValue = stringValues[index];

            EditorGUI.EndProperty();
        }
    }
}
#endif