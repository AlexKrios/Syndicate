using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using Syndicate.Utils;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Syndicate.Core.Configurations
{
    [CustomEditor(typeof(RawSetScriptable))]
    [CanEditMultipleObjects]
    public class RawSetScriptableEditor : Editor
    {
        private RawSetScriptable _data;

        private readonly List<bool> _infoItemFoldout = new();

        private bool _recipeFoldout;
        private bool _specificationsFoldout;

        private void Awake()
        {
            _data = (RawSetScriptable) target;

            for (var i = 0; i < _data.Items.Count; i++)
            {
                _infoItemFoldout.Add(false);
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
                _infoItemFoldout[index] = EditorGUILayout.Foldout(_infoItemFoldout[index], item.Name);
                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

                if (!_infoItemFoldout[index])
                    continue;

                CreateItem(item, index);
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateItem(RawItemScriptable data, int index)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            data.Name = EditorGUILayout.TextField("Name", data.Name);
            data.Key = (RawId)EditorGUILayout.TextField("Key", data.Key);
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
        }
    }
}
#endif