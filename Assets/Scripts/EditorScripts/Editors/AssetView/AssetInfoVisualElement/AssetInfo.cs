using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetInfo
    {
        public readonly string AssetName;

        public readonly Texture2D AssetPreviewImage;

        public AssetInfo(string assetName, Texture2D assetPreviewImage)
        {
            AssetName = assetName;
            AssetPreviewImage = assetPreviewImage;
        }
    }
}