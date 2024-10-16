using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgBrushes
{
    public class BrushVisualElement : VisualElement
    {
        private const string LayoutPath =
            "Assets/Scripts/EditorScripts/Editors/AssetLibrary/AgBrushes/BrushVisualElement.uxml";
        private const string BrushLabelName = "Brush-Name-Label";
        private const string BrushTypeName = "Brush-Type-Label";
        private const string BrushPreviewImageName = "Brush-Preview-Image";

        private readonly TemplateContainer _root;

        public BrushVisualElement()
        {
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
            if (visualTree is null)
            {
                Debug.LogError("Cant find layout for Brush Visual Element. \n" +
                               $"Path in script: {LayoutPath}");
                return;
            }
            TemplateContainer root = visualTree.CloneTree();

            Add(root);
            _root = root;
        }

        public void BindToBrushInfoVisualElement(AgBrush agBrush)
        {
            _root.Q<Image>(BrushPreviewImageName).image = agBrush.PreviewImage;
            _root.Q<Label>(BrushTypeName).text = agBrush.ToString();
            _root.Q<Label>(BrushLabelName).text = agBrush.BrushName;
        }
    }
}