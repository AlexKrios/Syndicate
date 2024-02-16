using System;
using System.Linq;
using System.Reflection;
using Syndicate.Core.Entities;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Drawers
{
    [CustomPropertyDrawer(typeof(ProductGroupId))]
    public class ProductIdDrawer : PropertyDrawer
    {
        private const string ProductGroupLabel = "Group Id: ";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.indentLevel = 0;

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var stringIds = typeof(ProductGroupId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();

            var index = Mathf.Max(0, Array.IndexOf(stringIds, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, ProductGroupLabel, index, stringIds);
            valueProperty.stringValue = stringIds[index];

            EditorGUI.EndProperty();
        }
    }
}