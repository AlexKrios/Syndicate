#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(UnitSideId))]
    public class UnitSideIdDrawer : PropertyDrawer
    {
        private const string UnitSideLabel = "Unit Side";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var stringValues = typeof(UnitSideId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();

            var index = Mathf.Max(0, Array.IndexOf(stringValues, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, UnitSideLabel, index, stringValues);
            valueProperty.stringValue = stringValues[index];

            EditorGUI.EndProperty();
        }
    }
}
#endif