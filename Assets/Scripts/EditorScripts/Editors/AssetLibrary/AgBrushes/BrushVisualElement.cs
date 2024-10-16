using UnityEditor;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgBrushes
{
    public class BrushVisualElement : VisualElement
    {
        private const string LayoutPath =
            "Assets/Scripts/EditorScripts/Editors/AssetLibrary/AgBrush/BrushVisualElement.uxml";
        private const string BrushLabelName = "Asset-Name-Label";
        private const string AssetPreviewImageName = "Asset-Preview-Image";

        private readonly TemplateContainer _root;

        public BrushVisualElement()
        {
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
            TemplateContainer root = visualTree.CloneTree();

            Add(root);
            _root = root;
        }

        public void BindToBrushInfoVisualElement(AgBrush agBrush)
        {
            
        }
    }
}