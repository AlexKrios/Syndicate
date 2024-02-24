using UnityEditor;

namespace Syndicate.Core.Entities
{
    [CustomPropertyDrawer(typeof(SpriteAssetId))]
    public class SpriteAssetIdDrawer : PropertyDrawer
    {
        /*private const string AudioLabel = "Audio Id: ";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var assetGUID = AssetDatabase.FindAssets($"t:{nameof(AudioSetScriptable)}").First();
            var path = AssetDatabase.GUIDToAssetPath(assetGUID);
            var allValues = AssetDatabase.LoadAssetAtPath<AudioSetScriptable>(path).Items;
            var selectedValues = allValues.Select(x => x.Id.ToString()).ToArray();

            var valueRect = new Rect(position);
            var valueProperty = property.FindPropertyRelative("value");
            var index = Mathf.Max(0, Array.IndexOf(allValues.ToArray(), valueProperty.stringValue));
            index = EditorGUI.Popup(valueRect, AudioLabel, index, selectedValues);
            valueProperty.stringValue = selectedValues[index];

            EditorGUI.EndProperty();
        }*/
    }
}