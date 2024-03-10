using System;
using Syndicate.Utils;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(PartObject))]
    public class PartObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fullPosition = position.width;

            position.width = 100;
            var itemTypeProperty = property.FindPropertyRelative("itemType").FindPropertyRelative("value");
            var itemIdProperty = property.FindPropertyRelative("itemId");
            var countProperty = property.FindPropertyRelative("count");

            var itemValues = EntitiesUtil.GetItemValues();
            var itemIndex = Mathf.Max(0, Array.IndexOf(itemValues, itemTypeProperty.stringValue));
            itemIndex = EditorGUI.Popup(position, itemIndex, itemValues);
            itemTypeProperty.stringValue = itemValues[itemIndex];

            position.x += position.width + 5;
            position.width = fullPosition - 150;
            var stringValues = GetValues((ItemTypeId)itemTypeProperty.stringValue);
            var index = Mathf.Max(0, Array.IndexOf(stringValues, itemIdProperty.stringValue));
            index = EditorGUI.Popup(position, index, stringValues);
            itemIdProperty.stringValue = stringValues[index];

            position.x += position.width + 5;
            position.width = 40;
            EditorGUI.PropertyField(position, countProperty, GUIContent.none);
        }

        private static string[] GetValues(ItemTypeId itemTypeId)
        {
            if (itemTypeId == ItemTypeId.Raw)
                return EntitiesUtil.GetRawValues();

            if (itemTypeId == ItemTypeId.Component)
                return EntitiesUtil.GetComponentValues();

            if (itemTypeId == ItemTypeId.Product)
                return EntitiesUtil.GetProductValues();

            return null;
        }
    }
}