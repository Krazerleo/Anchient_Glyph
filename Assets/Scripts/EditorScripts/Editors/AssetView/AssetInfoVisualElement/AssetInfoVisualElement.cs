using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetInfoVisualElement : VisualElement
    {
        private const string UxmlPath = "Assets/Scripts/EditorScripts/Editors/AssetView/AssetInfoVisualElement/AssetInfoVisualElement.uxml";
        private const string AssetLabelName = "Asset-Name-Label";
        private const string AssetPreviewImageName = "Asset-Preview-Image";

        private TemplateContainer _root;

        public AssetInfoVisualElement()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            var root = visualTree.CloneTree();

            Add(root);
            _root = root;
        }

        public void BindToAssetInfoVisualElement(string assetName, Texture2D assetPreviewImage)
        {
            _root.Q<Image>(AssetPreviewImageName).image = assetPreviewImage;
            _root.Q<Label>(AssetLabelName).text = assetName;
        }
    }
}