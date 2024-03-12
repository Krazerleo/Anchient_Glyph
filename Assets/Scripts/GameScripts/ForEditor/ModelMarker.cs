using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using UnityEngine;

namespace AncientGlyph.GameScripts.ForEditor
{
    /// <summary>
    /// Helper component for level model manipulation
    /// for instantiated objects. Removed after game has built.
    /// </summary>
    [ExecuteInEditMode]
    [SelectionBase]
    public class ModelMarker : MonoBehaviour
    {
        public Vector3Int Coordinates;
        public Direction Direction;
        public AssetType Type;
        public CreatureModel CreatureModel;
    }
}