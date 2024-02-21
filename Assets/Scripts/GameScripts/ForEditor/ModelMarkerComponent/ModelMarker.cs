using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities;
using UnityEngine;

namespace AncientGlyph.GameScripts.ForEditor.ModelMarkerComponent
{
    /// <summary>
    /// Helper component for level model manipulation
    /// for instantiated objects. Removed after game has built.
    /// </summary>
    [ExecuteInEditMode]
    public class ModelMarker : MonoBehaviour
    {
        public Vector3Int Coordinates;
        public Direction Direction;
        public AssetType Type;
        public CreatureModel CreatureModel;
    }
}