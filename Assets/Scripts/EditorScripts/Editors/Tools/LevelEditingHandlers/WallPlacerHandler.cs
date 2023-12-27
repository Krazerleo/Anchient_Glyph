using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Geometry.Extensions;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class WallPlacerHandler : IAssetPlacerHandler
    {
        #region Private Fields

        private Vector3 _firstPosition;
        private LevelModelEditor _levelEditor;
        private LevelSceneEditor _sceneEditor;
        private Vector3 _secondPosition;
        private GameObject _wallPrefab;

        #endregion Private Fields

        #region Public Constructors

        public WallPlacerHandler()
        {
            _levelEditor = new LevelModelEditor();
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

            if (Mathf.Abs(diagonalVector.x) > Mathf.Abs(diagonalVector.z))
            {
                if (Mathf.RoundToInt(_firstPosition.z) == Mathf.FloorToInt(_firstPosition.z))
                {
                    _sceneEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetZInt(Mathf.FloorToInt(_firstPosition.z))
                        ), _wallPrefab, Direction.South);

                    _levelEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetZInt(Mathf.FloorToInt(_firstPosition.z))
                        ), Direction.South);
                }
                else
                {
                    _sceneEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetZInt(Mathf.FloorToInt(_firstPosition.z))
                        ), _wallPrefab, Direction.North);

                    _levelEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetZInt(Mathf.FloorToInt(_firstPosition.z))
                        ), Direction.North);
                }
            }
            else
            {
                if (Mathf.RoundToInt(_firstPosition.x) == Mathf.FloorToInt(_firstPosition.x))
                {
                    _sceneEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetXInt(Mathf.FloorToInt(_firstPosition.x))
                        ), _wallPrefab, Direction.West);

                    _levelEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetXInt(Mathf.FloorToInt(_firstPosition.x))
                        ), Direction.West);
                }
                else
                {
                    _sceneEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetXInt(Mathf.FloorToInt(_firstPosition.x))
                        ), _wallPrefab, Direction.East);

                    _levelEditor.PlaceWall(new Rectangle(
                        _firstPosition.ToVector3Int(), _secondPosition.ToVector3Int().SetXInt(Mathf.FloorToInt(_firstPosition.x))
                        ), Direction.East);
                }
            }
        }

        #endregion Public Methods
    }
}