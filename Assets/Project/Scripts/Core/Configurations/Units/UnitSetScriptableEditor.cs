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

            EditorGUILayout.Space();

            var unitsString = EntitiesUtil.GetUnitTypeValues();
            var componentIndex = Mathf.Max(0, Array.IndexOf(unitsString, data.UnitTypeId));
            componentIndex = EditorGUILayout.Popup("Product Group", componentIndex, unitsString);
            data.UnitTypeId = (UnitTypeId)unitsString[componentIndex];

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

            EditorGUILayout.Space(10);
            CreateSpecifications(data.Specifications);

            EditorGUILayout.EndVertical();
        }

        private void CreateSpecifications(List<SpecificationObject> data)
        {
            var specificationValues = EntitiesUtil.GetSpecificationValues();
            if (data.Count < specificationValues.Length)
            {
                for (var i = data.Count; i < specificationValues.Length; i++)
                {
                    data.Add(new SpecificationObject { Type = (SpecificationId) specificationValues[i] });
                }
            }

            EditorGUILayout.BeginHorizontal();
            foreach (var specification in data)
            {
                EditorGUILayout.BeginVertical();
                EditorGUI.BeginDisabledGroup(true);
                var itemIndex = Mathf.Max(0, Array.IndexOf(specificationValues, specification.Type));
                itemIndex = EditorGUILayout.Popup(itemIndex, specificationValues);
                specification.Type = (SpecificationId)specificationValues[itemIndex];
                EditorGUI.EndDisabledGroup();

                specification.Value = EditorGUILayout.IntField(specification.Value);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif