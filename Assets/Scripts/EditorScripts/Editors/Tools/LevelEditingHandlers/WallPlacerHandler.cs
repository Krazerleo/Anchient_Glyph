using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Geometry.Extensions;

using UnityEngine;
using AncientGlyph.EditorScripts.Editors.UndoRedo;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class WallPlacerHandler : IAssetPlacerHandler
    {
        private Vector3 _firstPosition;
        private LevelModelEditor _levelEditor;
        private LevelSceneEditor _sceneEditor;
        private Vector3 _secondPosition;
        private GameObject _wallPrefab;

        public WallPlacerHandler()
        {
            _levelEditor = new LevelModelEditor();
            _sceneEditor = new LevelSceneEditor();
        }

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
                var xRectangle = new Rectangle(
                                _firstPosition.ToVector3Int(),
                                _secondPosition.ToVector3Int()
                                    .SetZInt(Mathf.FloorToInt(_firstPosition.z)));

                if (Mathf.RoundToInt(_firstPosition.z) == Mathf.FloorToInt(_firstPosition.z))
                {
                    if (_levelEditor.TryPlaceWall(xRectangle, Direction.South))
                    {
                        _sceneEditor.PlaceWall(xRectangle, _wallPrefab, Direction.South);
                    }
                }
                else
                {
                    if (_levelEditor.TryPlaceWall(xRectangle, Direction.North))
                    {
                        _sceneEditor.PlaceWall(xRectangle, _wallPrefab, Direction.North);
                    }
                }
            }
            else
            {
                var zRectangle = new Rectangle(
                                _firstPosition.ToVector3Int(),
                                _secondPosition.ToVector3Int()
                                    .SetXInt(Mathf.FloorToInt(_firstPosition.x)));

                if (Mathf.RoundToInt(_firstPosition.x) == Mathf.FloorToInt(_firstPosition.x))
                {
                    if (_levelEditor.TryPlaceWall(zRectangle, Direction.West))
                    {
                        _sceneEditor.PlaceWall(zRectangle, _wallPrefab, Direction.West);
                    }
                }
                else
                {
                    if (_levelEditor.TryPlaceWall(zRectangle, Direction.East))
                    {
                        _sceneEditor.PlaceWall(zRectangle, _wallPrefab, Direction.East);
                    }
                }
            }
        }
    }
}