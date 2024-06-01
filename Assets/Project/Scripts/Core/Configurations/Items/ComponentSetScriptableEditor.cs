using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using Syndicate.Utils;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Syndicate.Core.Configurations
{
    [CustomEditor(typeof(ComponentSetScriptable))]
    [CanEditMultipleObjects]
    public class ComponentScriptableEditor : Editor
    {
        private ComponentSetScriptable _data;

        private readonly List<bool> _infoFoldout = new();
        private bool _recipeFoldout;
        private bool _specificationsFoldout;

        private void Awake()
        {
            _data = (ComponentSetScriptable) target;

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

                CreateProduct(item, index);
                CreateRecipe(item.Recipe);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateProduct(ComponentScriptable data, int index)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Info", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            data.Name = EditorGUILayout.TextField("Name", data.Name);
            data.Key = (ComponentId)EditorGUILayout.TextField("Key", data.Key);

            EditorGUILayout.Space();

            var componentsString = EntitiesUtil.GetProductGroupValues();
            var componentIndex = Mathf.Max(0, Array.IndexOf(componentsString, data.ProductGroupId));
            componentIndex = EditorGUILayout.Popup("Product Group", componentIndex, componentsString);
            data.ProductGroupId = (ProductGroupId)componentsString[componentIndex];

            var unitsString = EntitiesUtil.GetUnitTypeValues();
            var unitIndex = Mathf.Max(0, Array.IndexOf(unitsString, data.UnitTypeId));
            unitIndex = EditorGUILayout.Popup("Unit Type", unitIndex, unitsString);
            data.UnitTypeId = (UnitTypeId)unitsString[unitIndex];

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

        private void CreateRecipe(RecipeObject recipe)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Recipe", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal();
            var styleBox = new GUIStyle(GUI.skin.label) { fixedWidth = 18, fixedHeight = 18 };
            GUILayout.Box(Resources.Load<Texture2D>("money"), styleBox);
            recipe.Cost = EditorGUILayout.IntField(recipe.Cost);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Box(Resources.Load<Texture2D>("experience"), styleBox);
            recipe.Experience = EditorGUILayout.IntField(recipe.Experience);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Box(Resources.Load<Texture2D>("timer"), styleBox);
            recipe.CraftTime = EditorGUILayout.IntField(recipe.CraftTime);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            CreateParts(recipe.Parts);
            EditorGUILayout.Space();
            CreateSpecifications(recipe.Specifications);

            EditorGUILayout.EndVertical();
        }

        private void CreateParts(List<PartObject> parts)
        {
            foreach (var part in parts)
            {
                EditorGUILayout.BeginHorizontal();
                var partIndex = parts.IndexOf(part);
                var optionsPart = new[] { GUILayout.MaxWidth(50f), GUILayout.MinWidth(10f) };
                EditorGUILayout.LabelField($"Part {partIndex + 1}:", optionsPart);

                var optionsType = new[] { GUILayout.MaxWidth(100f), GUILayout.MinWidth(10f) };
                part.ItemType = (ItemType) EditorGUILayout.EnumPopup(part.ItemType, optionsType);

                var idsValues = GetKeys(part.ItemType);
                var index = Mathf.Max(0, Array.IndexOf(idsValues, part.Key));
                index = EditorGUILayout.Popup(index, idsValues);
                part.Key = idsValues[index];

                var options = new[] { GUILayout.MaxWidth(50f), GUILayout.MinWidth(10f) };
                part.Count = EditorGUILayout.IntField(part.Count, options);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            if (parts.Count < 3)
            {
                if(GUILayout.Button("Add new part"))
                    parts.Add(new PartObject());
            }

            if (parts.Count != 0)
            {
                if (GUILayout.Button("Remove last part"))
                    parts.RemoveAt(parts.Count - 1);
            }
            EditorGUILayout.EndHorizontal();
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

        private static string[] GetKeys(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Raw => EntitiesUtil.GetRawItemKeys(),
                ItemType.Component => EntitiesUtil.GetComponentItemKeys(),
                ItemType.Product => EntitiesUtil.GetProductItemKeys(),
                _ => null
            };
        }
    }
}
#endif