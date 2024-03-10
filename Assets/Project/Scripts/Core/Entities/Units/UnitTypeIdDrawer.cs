using System;
using System.Linq;
using System.Reflection;
using Syndicate.Core.Entities;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Drawers
{
    [CustomPropertyDrawer(typeof(UnitTypeId))]
    public class UnitTypeIdDrawer : PropertyDrawer
    {
        private const string UnitTypeLabel = "Unit Type Id";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var stringValues = typeof(UnitTypeId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();

            var index = Mathf.Max(0, Array.IndexOf(stringValues, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, UnitTypeLabel, index, stringValues);
            valueProperty.stringValue = stringValues[index];

            EditorGUI.EndProperty();
        }
    }
}