using AncientGlyph.EditorScripts.Editors.EditorComponent.Interfaces;
using AncientGlyph.GameScripts.Enums;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.EditorComponent
{
    /// <summary>
    /// Helper component for level model manipulation (maybe more)
    /// for instantiated objects. Removed after game has builded.
    /// </summary>
    public class ModelMarkerComponent : MonoBehaviour, IEditorOnlyMarker
    {
        #region Private Fields

        private Vector3Int _coordinates;
        private string _creatureId;
        private Direction _direction;
        private LevelModelEditor _levelEditor;
        private AssetType _type;

        #endregion Private Fields

        #region Public Methods

        public void ConstructForCreatureAsset(Vector3Int coordinates, LevelModelEditor modelEditor)
        {
            _type = AssetType.Creature;
            _coordinates = coordinates;
            _levelEditor = modelEditor;
        }

        public void ConstructForTileAsset(Vector3Int coordinates, LevelModelEditor modelEditor)
        {
            _type = AssetType.Creature;
            _coordinates = coordinates;
            _levelEditor = modelEditor;
        }

        public void ConstructForWallAsset(Vector3Int coordinates, Direction direction, LevelModelEditor modelEditor)
        {
            _type = AssetType.Wall;
            _coordinates = coordinates;
            _direction = direction;
            _levelEditor = modelEditor;
        }

        #endregion Public Methods

        #region Unity Messages

        private void OnDestroy()
        {
            switch (_type)
            {
                case AssetType.Tile:
                {
                    _levelEditor.RemoveTile(_coordinates);
                    break;
                }
                case AssetType.Wall:
                {
                    _levelEditor.RemoveWall(_coordinates, _direction);
                    break;
                }
                case AssetType.Creature:
                {
                    _levelEditor.RemoveEntity(_coordinates, _creatureId);
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        #endregion
    }
}