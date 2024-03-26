using System;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEditor;
using UnityEngine;

namespace AncientGlyph.GameScripts.ForEditor
{
    /// <summary>
    /// Helper component for level model manipulation
    /// for instantiated objects. Removed after game has built.
    /// </summary>
    [SelectionBase]
    public class ModelMarker : MonoBehaviour
    {
        public Vector3 ItemCoordinates;
        public string GameItemIdentifier;

        public Vector3Int Coordinates;
        public Direction Direction;
        public AssetType Type;
        public CreatureModel CreatureModel;
    }

    [CustomEditor(typeof(ModelMarker))]
    public class MarkerLabel : Editor
    {
        private GUIStyle _itemGuiStyle;
        private GUIStyle _entityGuiStyle;

        private void OnEnable()
        {
            _itemGuiStyle = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.green,
                },
                fontSize = 16,
            };
            
            _entityGuiStyle = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.blue,
                },
                fontSize = 16,
            };
        }

        public void OnSceneGUI()
        {
            var marker = (ModelMarker)target;

            using var handlesScope = new Handles.DrawingScope();

            if (marker.Type == AssetType.Item)
            {
                Handles.Label(marker.transform.position, 
                    marker.GameItemIdentifier, _itemGuiStyle);
            }
            
            if (marker.Type == AssetType.Entity)
            {
                Handles.Label(marker.transform.position, 
                    marker.CreatureModel.SerializationName, _entityGuiStyle);
            }
        }
    }
}