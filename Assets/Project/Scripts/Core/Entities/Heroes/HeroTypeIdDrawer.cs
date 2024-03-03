using System;
using System.Linq;
using System.Reflection;
using Syndicate.Core.Entities;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Drawers
{
    [CustomPropertyDrawer(typeof(HeroTypeId))]
    public class HeroTypeIdDrawer : PropertyDrawer
    {
        private const string HeroTypeLabel = "Hero Type Id";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.indentLevel = 0;

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var stringIds = typeof(HeroTypeId).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null).ToString())
                .ToArray();

            var index = Mathf.Max(0, Array.IndexOf(stringIds, valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, HeroTypeLabel, index, stringIds);
            valueProperty.stringValue = stringIds[index];

            EditorGUI.EndProperty();
        }
    }
}