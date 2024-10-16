using UnityEditor;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAsset
{
    public class AssetVisualElement : VisualElement
    {
        private const string LayoutPath =
            "Assets/Scripts/EditorScripts/Editors/AssetLibrary/AgAsset/AssetInfoVisualElement.uxml";
        private const string AssetLabelName = "Asset-Name-Label";
        private const string AssetPreviewImageName = "Asset-Preview-Image";

        private readonly TemplateContainer _root;

        public AssetVisualElement()
        {
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
            TemplateContainer root = visualTree.CloneTree();

            Add(root);
            _root = root;
        }

        public void BindToAssetInfoVisualElement(AssetViewModel assetViewModel)
        {
            _root.Q<Image>(AssetPreviewImageName).image = assetViewModel.AssetPreviewImage;
            _root.Q<Label>(AssetLabelName).text = assetViewModel.PrefabFile.Name;
        }
    }
}