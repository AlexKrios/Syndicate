using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(SpecificationId))]
    public class SpecificationIdDrawer : PropertyDrawer
    {
        private const string SpecificationLabel = "Specification Id";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var stringValues = typeof(SpecificationId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();

            var index = Mathf.Max(0, Array.IndexOf(stringValues, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, SpecificationLabel, index, stringValues);
            valueProperty.stringValue = stringValues[index];

            EditorGUI.EndProperty();
        }
    }
}