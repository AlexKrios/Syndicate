#if UNITY_EDITOR
using System;
using Syndicate.Utils;
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

            var stringValues = EntitiesUtil.GetSpriteAssetValues();
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