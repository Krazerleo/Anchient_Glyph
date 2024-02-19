using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetViewLibrary.AssetInfoVisualElement
{
    public class AssetInfoVisualElement : VisualElement
    {
        private const string LayoutPath = "Assets/Scripts/EditorScripts/Editors/AssetViewLibrary/AssetInfoVisualElement/AssetInfoVisualElement.uxml";
        private const string AssetLabelName = "Asset-Name-Label";
        private const string AssetPreviewImageName = "Asset-Preview-Image";

        private readonly TemplateContainer _root;

        public AssetInfoVisualElement()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
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