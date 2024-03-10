
using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using Syndicate.Utils;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Syndicate.Core.Configurations
{
    [CustomEditor(typeof(ProductSetScriptable))]
    [CanEditMultipleObjects]
    public class ProductSetScriptableEditor : Editor
    {
        private ProductSetScriptable _data;

        private readonly List<bool> _infoFoldout = new();
        private bool _recipeFoldout;
        private bool _specificationsFoldout;

        private void Awake()
        {
            _data = (ProductSetScriptable) target;

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
                CreateParts(item.Recipe.Parts);
                CreateRecipe(item.Recipe);
                CreateSpecifications(item.Recipe.Specifications);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateProduct(ProductScriptable data, int index)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Info", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            data.Name = EditorGUILayout.TextField("Name", data.Name);
            data.Key = (ProductId)EditorGUILayout.TextField("Key", data.Key);
            data.Id = EditorGUILayout.TextField("Id", data.Id);

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

        private void CreateParts(List<PartObject> parts)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Parts", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            foreach (var part in parts)
            {
                EditorGUILayout.BeginHorizontal();
                var partIndex = parts.IndexOf(part);
                var optionsPart = new[] { GUILayout.MaxWidth(75f), GUILayout.MinWidth(10f) };
                EditorGUILayout.LabelField($"Part {partIndex + 1}:", optionsPart);

                var itemValues = EntitiesUtil.GetItemValues();
                var itemIndex = Mathf.Max(0, Array.IndexOf(itemValues, part.ItemType));
                var optionsType = new[] { GUILayout.MaxWidth(100f), GUILayout.MinWidth(10f) };
                itemIndex = EditorGUILayout.Popup(itemIndex, itemValues, optionsType);
                part.ItemType = (ItemTypeId)itemValues[itemIndex];

                var idsValues = GetValues(part.ItemType);
                var index = Mathf.Max(0, Array.IndexOf(idsValues, part.ItemId));
                index = EditorGUILayout.Popup(index, idsValues);
                part.ItemId = idsValues[index];

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
            EditorGUILayout.EndVertical();
        }

        private void CreateRecipe(RecipeObject recipe)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Recipe", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            recipe.Cost = EditorGUILayout.IntField("Cost", recipe.Cost);
            recipe.Experience = EditorGUILayout.IntField("Experience", recipe.Experience);
            recipe.CraftTime = EditorGUILayout.IntField("Craft time", recipe.CraftTime);
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

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Specifications", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

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
            EditorGUILayout.EndVertical();
        }

        private static string[] GetValues(ItemTypeId itemTypeId)
        {
            if (itemTypeId == ItemTypeId.Raw)
                return EntitiesUtil.GetRawValues();

            if (itemTypeId == ItemTypeId.Component)
                return EntitiesUtil.GetComponentValues();

            if (itemTypeId == ItemTypeId.Product)
                return EntitiesUtil.GetProductValues();

            return null;
        }
    }
}
#endif