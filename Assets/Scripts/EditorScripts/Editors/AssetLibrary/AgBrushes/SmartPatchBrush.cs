using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgBrushes
{
    [CreateAssetMenu(fileName = "AG Smart Patch Brush", menuName = "AG Assets/Brushes/Smart Patch Brush")]
    public class SmartPatchBrush : AgBrush
    {
        public override GameObject PlaceAsset(Vector3Int position)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString() => "Smart Patch Brush";
    }
}