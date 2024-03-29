#if UNITY_EDITOR
using System;
using Syndicate.Utils;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(SpecificationObject))]
    public class SpecificationObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fullPosition = position.width;

            position.width = fullPosition - 60;
            var typeProperty = property.FindPropertyRelative("type").FindPropertyRelative("value");
            var valueProperty = property.FindPropertyRelative("value");

            var itemValues = EntitiesUtil.GetSpecificationValues();
            var itemIndex = Mathf.Max(0, Array.IndexOf(itemValues, typeProperty.stringValue));
            itemIndex = EditorGUI.Popup(position, itemIndex, itemValues);
            typeProperty.stringValue = itemValues[itemIndex];

            position.x += position.width + 5;
            position.width = 50;
            EditorGUI.PropertyField(position, valueProperty, GUIContent.none);
        }
    }
}
#endif