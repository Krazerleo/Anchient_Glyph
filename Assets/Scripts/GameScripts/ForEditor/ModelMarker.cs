using System;

using AncientGlyph.GameScripts.Enums;

using UnityEngine;

namespace AncientGlyph.GameScripts.ForEditor
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
        public EventHandler<ModelMarker> DeletionHandler;

        private void OnDestroy()
        {
            DeletionHandler?.Invoke(null, this);
        }
    }
}