using System.IO;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAsset
{
    public class AssetViewModel
    {
        public readonly FileInfo PrefabFile;
        public readonly Texture2D AssetPreviewImage;

        public AssetViewModel(FileInfo prefabFile, Texture2D assetPreviewImage)
        {
            PrefabFile = prefabFile;
            AssetPreviewImage = assetPreviewImage;
        }
    }
}