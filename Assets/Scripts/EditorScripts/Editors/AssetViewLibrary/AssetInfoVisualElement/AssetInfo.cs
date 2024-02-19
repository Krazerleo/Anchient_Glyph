using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetViewLibrary.AssetInfoVisualElement
{
    public class AssetInfo
    {
        public readonly string AssetName;
        public readonly string AssetPath;
        public readonly Texture2D AssetPreviewImage;

        public AssetInfo(string assetName, Texture2D assetPreviewImage, string assetPath)
        {
            AssetName = assetName;
            AssetPreviewImage = assetPreviewImage;
            AssetPath = assetPath;
        }
    }
}