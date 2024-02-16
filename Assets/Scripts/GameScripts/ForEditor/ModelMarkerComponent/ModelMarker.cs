using AncientGlyph.GameScripts.Enums;

using UnityEngine;
using UltEvents;

namespace AncientGlyph.GameScripts.ForEditor.ModelMarkerComponent
{
    /// <summary>
    /// Helper component for level model manipulation
    /// for instantiated objects. Removed after game has builded.
    /// </summary>
    [ExecuteInEditMode]
    public class ModelMarker : MonoBehaviour
    {
        public Vector3Int Coordinates;
        public string EntityId;
        public Direction Direction;
        public AssetType Type;
        public UltEvent<ModelMarker> DeletionHandler;

        private void OnDestroy()
        {
            DeletionHandler?.Invoke(this);
        }
    }
}