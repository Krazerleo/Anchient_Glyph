using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Geometry;

using UnityEngine;
using UnityEditor;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers
{
    public class WallPlacerHandler : IAssetPlacerHandler
    {
        private const string GhostWallAssetPath = "Assets/Level/Prefab/Debug/wall_ghost.prefab";

        private Vector3 _startPosition;
        private LevelModelEditor _levelEditor = new();
        private LevelSceneEditor _sceneEditor = new();
        private Vector3 _lastPosition;
        private GameObject _wallPrefab;

        private bool _isJustStarted = false;

        public void OnMouseButtonPressedHandler(Vector3 startMousePosition)
        {
            _startPosition = startMousePosition;
            _lastPosition = startMousePosition;
            _isJustStarted = true;
        }

        // Ghost wall placement
        public void OnMouseMoveHandler(Vector3 lastMousePosition)
        {
            if (_wallPrefab == null)
            {
                return;
            }

            if (_lastPosition == lastMousePosition.ToVector3Int())
            {
                return;
            }

            _lastPosition = lastMousePosition.ToVector3Int();

            var ghostWallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(GhostWallAssetPath);

            var (walls, dir) = ResolveWallsAndDirection();

            //Remove previous ghost walls
            //In case if they exists
            if (_isJustStarted)
            {
                _isJustStarted = false;
            }
            else
            {
                Undo.PerformUndo();
            }

            _sceneEditor.PlaceWalls(walls, ghostWallPrefab, dir);
        }

        // Actual ghost wall placement
        public void OnMouseButtonReleasedHandler(Vector3 lastMousePosition)
        {
            if (_wallPrefab == null)
            {
                return;
            }

            _lastPosition = lastMousePosition;
            var (walls, dir) = ResolveWallsAndDirection();

            //Remove left ghosted walls
            Undo.PerformUndo();

            if (_levelEditor.TryPlaceWall(walls, dir))
            {
                _sceneEditor.PlaceWalls(walls, _wallPrefab, dir);
            }
        }

        private (Rectangle wallRectangle, Direction direction) ResolveWallsAndDirection()
        {
            var diagonalVector = _lastPosition - _startPosition;

            if (Mathf.Abs(diagonalVector.x) > Mathf.Abs(diagonalVector.z))
            {
                var xRectangle = new Rectangle(
                                _startPosition.ToVector3Int(),
                                _lastPosition.ToVector3Int()
                                    .SetZInt(Mathf.FloorToInt(_startPosition.z)));

                if (Mathf.RoundToInt(_startPosition.z) == Mathf.FloorToInt(_startPosition.z))
                {
                    return (xRectangle, Direction.South);
                }
                else
                {
                    return (xRectangle, Direction.North);
                }
            }
            else
            {
                var zRectangle = new Rectangle(
                                _startPosition.ToVector3Int(),
                                _lastPosition.ToVector3Int()
                                    .SetXInt(Mathf.FloorToInt(_startPosition.x)));

                if (Mathf.RoundToInt(_startPosition.x) == Mathf.FloorToInt(_startPosition.x))
                {
                    return (zRectangle, Direction.West);
                }
                else
                {
                    return (zRectangle, Direction.East);
                }
            }
        }

        public void SetPrefabObject(GameObject prefab)
        {
            _wallPrefab = prefab;
        }
    }
}