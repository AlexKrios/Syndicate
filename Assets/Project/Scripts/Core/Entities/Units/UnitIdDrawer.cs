#if UNITY_EDITOR
using System;
using System.Linq;
using Syndicate.Core.Configurations;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(UnitId))]
    public class UnitIdDrawer : PropertyDrawer
    {
        private const string UnitIdLabel = "Unit Id";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var assetId = AssetDatabase.FindAssets($"t:{nameof(UnitSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetId);
            var allValues = AssetDatabase.LoadAssetAtPath<UnitSetScriptable>(path).Items;
            var stringValues = allValues.Select(x => x.Key.ToString()).ToArray();

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var index = Mathf.Max(0, Array.IndexOf(stringValues, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, UnitIdLabel, index, stringValues);
            valueProperty.stringValue = stringValues[index];

            EditorGUI.EndProperty();
        }
    }
}
#endif