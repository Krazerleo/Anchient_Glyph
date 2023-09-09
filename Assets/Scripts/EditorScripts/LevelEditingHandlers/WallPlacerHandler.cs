using AncientGlyph.EditorScripts.Editors;
using AncientGlyph.EditorScripts.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Geometry.Extensions;

using UnityEngine;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class WallPlacerHandler : IAssetPlacerHandler
    {
        #region Private Fields

        private Vector3 _firstPosition;
        private LevelModelEditor _modelEditor;
        private LevelSceneEditor _sceneEditor;
        private Vector3 _secondPosition;
        private GameObject _wallPrefab;

        #endregion Private Fields

        #region Public Constructors

        public WallPlacerHandler(LevelModel levelModel, GameObject wallPrefab)
        {
            _wallPrefab = wallPrefab;
            _modelEditor = new LevelModelEditor(levelModel);
            _sceneEditor = new LevelSceneEditor();
        }

        #endregion Public Constructors

        #region Public Methods

        public void OnMouseButtonClickHandler(Vector3 mousePosition)
        {
        }

        public void OnMouseButtonPressedHandler(Vector3 firstMousePosition)
        {
            _firstPosition = firstMousePosition;
        }

        public void OnMouseButtonReleasedHandler(Vector3 secondMousePosition)
        {
            _secondPosition = secondMousePosition;
            ResolveWalls();
        }

        public void OnMouseMoveHandler(Vector3 position)
        {
        }

        public void SetPrefabObject(GameObject prefab)
        {
            _wallPrefab = prefab;
        }

        private void ResolveWalls()
        {
            if (_wallPrefab == null)
            {
                return;
            }

            var diagonalVector = _secondPosition - _firstPosition;

            // X axis -> East, Z axis -> North
            if (Mathf.Abs(diagonalVector.x) > Mathf.Abs(diagonalVector.z))
            {
                if (_firstPosition.x - Mathf.FloorToInt(_firstPosition.x) > 0.5)
                {
                    _sceneEditor
                        .PlaceWall(new Rectangle(_firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetZInt(Mathf.FloorToInt(_firstPosition.z))),
                        _wallPrefab, Direction.South);
                }
                else
                {
                    _sceneEditor
                        .PlaceWall(new Rectangle(_firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetZInt(Mathf.FloorToInt(_firstPosition.z))),
                        _wallPrefab, Direction.North);
                }
            }
            else
            {
                if (_firstPosition.z - Mathf.FloorToInt(_firstPosition.z) > 0.5)
                {
                    _sceneEditor
                        .PlaceWall(new Rectangle(_firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetXInt(Mathf.FloorToInt(_firstPosition.x))),
                        _wallPrefab, Direction.East);
                }
                else
                {
                    _sceneEditor
                        .PlaceWall(new Rectangle(_firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetXInt(Mathf.FloorToInt(_firstPosition.x))),
                        _wallPrefab, Direction.West);
                }
            }
        }

        #endregion Public Methods
    }
}