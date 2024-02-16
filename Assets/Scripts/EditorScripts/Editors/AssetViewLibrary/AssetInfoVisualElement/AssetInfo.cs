using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetInfo
    {
        #region Public Fields

        public readonly string AssetName;
        public readonly string AssetPath;
        public readonly Texture2D AssetPreviewImage;

        #endregion Public Fields

        #region Public Constructors

        public AssetInfo(string assetName, Texture2D assetPreviewImage, string assetPath)
        {
            AssetName = assetName;
            AssetPreviewImage = assetPreviewImage;
            AssetPath = assetPath;
        }

        #endregion Public Constructors
    }
}