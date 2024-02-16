using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using UnityEditor;
using UnityEngine;

namespace Syndicate.Core.Drawers
{
    [CustomPropertyDrawer(typeof(SoundAssetId))]
    public class SoundIdDrawer : PropertyDrawer
    {
        private const string SoundLabel = "Sound Id: ";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var allValues = new List<string>();

            var assetGUID = AssetDatabase.FindAssets($"t:{nameof(AssetsScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetGUID);
            var sounds = AssetDatabase.LoadAssetAtPath<AssetsScriptable>(path).Sounds;

            var propertyInfos = sounds.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var propertyList in propertyInfos)
            {
                if (propertyList.GetValue(sounds) is List<SoundScriptable> actualValue)
                    allValues.AddRange(actualValue.Select(x => x.Id.ToString()));
            }

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var index = Mathf.Max(0, Array.IndexOf(allValues.ToArray(), valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, SoundLabel, index, allValues.ToArray());
            valueProperty.stringValue = allValues[index];

            EditorGUI.EndProperty();
        }
    }
}