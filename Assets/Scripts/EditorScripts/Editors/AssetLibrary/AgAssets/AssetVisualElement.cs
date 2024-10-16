using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAssets
{
    public class AssetVisualElement : VisualElement
    {
        private const string LayoutPath =
            "Assets/Scripts/EditorScripts/Editors/AssetLibrary/AgAssets/AssetVisualElement.uxml";

        private const string AssetLabelName = "Asset-Name-Label";
        private const string AssetPreviewImageName = "Asset-Preview-Image";

        private readonly TemplateContainer _root;

        public AssetVisualElement()
        {
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
            if (visualTree is null)
            {
                Debug.LogError("Cant find layout for Asset Visual Element. \n" +
                               $"Path in script: {LayoutPath}");
                return;
            }

            TemplateContainer root = visualTree.CloneTree();

            Add(root);
            _root = root;
        }

        public void BindToAssetInfoVisualElement(AgAsset agAsset)
        {
            _root.Q<Image>(AssetPreviewImageName).image = agAsset.PreviewImage;
            _root.Q<Label>(AssetLabelName).text = string.IsNullOrEmpty(agAsset.AssetName)
                ? agAsset.Prefab.name
                : agAsset.AssetName;
        }
    }
}