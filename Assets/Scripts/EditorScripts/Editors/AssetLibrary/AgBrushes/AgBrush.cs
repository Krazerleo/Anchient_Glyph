using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgBrushes
{
    public abstract class AgBrush : ScriptableObject
    {
        public Texture2D PreviewImage;

        /// <summary>
        /// Determine which asset to place on position
        /// </summary>
        public abstract GameObject PlaceAsset(Vector3Int position);
    }
}