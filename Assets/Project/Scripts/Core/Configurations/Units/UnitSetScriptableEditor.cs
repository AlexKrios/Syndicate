using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using Syndicate.Utils;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Syndicate.Core.Configurations
{
    [CustomEditor(typeof(UnitSetScriptable))]
    [CanEditMultipleObjects]
    public class UnitSetScriptableEditor : Editor
    {
        private UnitSetScriptable _data;

        private readonly List<bool> _infoFoldout = new();
        private bool _recipeFoldout;
        private bool _specificationsFoldout;

        private void Awake()
        {
            _data = (UnitSetScriptable) target;

            for (var i = 0; i < _data.Count; i++)
            {
                _infoFoldout.Add(false);
            }

            EditorUtility.SetDirty(_data);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (var item in _data.Items)
            {
                var index = _data.Items.IndexOf(item);

                EditorGUILayout.BeginHorizontal(EditorStyles.objectFieldThumb);
                EditorGUI.indentLevel++;
                _infoFoldout[index] = EditorGUILayout.Foldout(_infoFoldout[index], item.Name);
                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

                if (!_infoFoldout[index])
                    continue;

                CreateUnit(item, index);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateUnit(UnitScriptable data, int index)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Info", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            data.Name = EditorGUILayout.TextField("Name", data.Name);
            data.Key = (UnitId)EditorGUILayout.TextField("Key", data.Key);

            var unitsString = EntitiesUtil.GetUnitTypeValues();
            var componentIndex = Mathf.Max(0, Array.IndexOf(unitsString, data.UnitTypeId));
            componentIndex = EditorGUILayout.Popup("Product Group", componentIndex, unitsString);
            data.UnitTypeId = (UnitTypeId)unitsString[componentIndex];

            EditorGUILayout.Space();

            var spritesString = EntitiesUtil.GetSpriteAssetValues();
            var spriteIndex = Mathf.Max(0, Array.IndexOf(spritesString, data.SpriteAssetId));
            spriteIndex = EditorGUILayout.Popup("Sprite Asset", spriteIndex, spritesString);
            data.SpriteAssetId = (SpriteAssetId)spritesString[spriteIndex];

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject
                .FindProperty("items")
                .GetArrayElementAtIndex(index)
                .FindPropertyRelative("nameLocale"), new GUIContent("Name Locale"));
            EditorGUI.indentLevel--;

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject
                .FindProperty("items")
                .GetArrayElementAtIndex(index)
                .FindPropertyRelative("descriptionLocale"), new GUIContent("Description Locale"));
            EditorGUI.indentLevel--;

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.BeginVertical();
            CreateStarInfo(data.Stars);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        private void CreateStarInfo(List<UnitStarScriptable> starDataList)
        {
            foreach (var starData in starDataList)
            {
                CreateSpecifications(starData);
            }
        }

        private void CreateSpecifications(UnitStarScriptable starData)
        {
            var data = starData.Specifications;
            var specificationValues = EntitiesUtil.GetSpecificationValues();
            if (data.Count < specificationValues.Length)
            {
                for (var i = data.Count; i < specificationValues.Length; i++)
                {
                    data.Add(new SpecificationObject { Type = (SpecificationId) specificationValues[i] });
                }
            }

            EditorGUILayout.BeginHorizontal();
            var styleBox = new GUIStyle(GUI.skin.label) { fixedWidth = 18, fixedHeight = 18 };
            GUILayout.Box(Resources.Load<Texture2D>("star"), styleBox);
            var optionsStarStyle = new[] { GUILayout.MaxWidth(20f), GUILayout.MinWidth(20f) };
            starData.Star = EditorGUILayout.IntField(starData.Star, optionsStarStyle);
            foreach (var specification in data)
            {
                var itemIndex = Mathf.Max(0, Array.IndexOf(specificationValues, specification.Type));
                EditorGUILayout.BeginHorizontal();
                GUILayout.Box(Resources.Load<Texture2D>($"Specifications/{(SpecificationId)specificationValues[itemIndex]}"), styleBox);
                EditorGUI.BeginDisabledGroup(true);
                specification.Type = (SpecificationId)specificationValues[itemIndex];
                EditorGUI.EndDisabledGroup();

                specification.Value = EditorGUILayout.IntField(specification.Value);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif