using AncientGlyph.GameScripts.Geometry;
using AncientGlyph.GameScripts.Geometry.Shapes;
using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.EditingHandlers
{
    public class TilePlacerHandler : IAssetPlacerHandler
    {
        private const string GhostTileAssetPath = "Assets/Level/Prefab/Debug/floor_ghost.prefab";

        private Vector3Int _startPosition;
        private Vector3Int _lastPosition;

        private readonly LevelSceneEditor _sceneEditor = new();
        private GameObject _tilePrefab;

        private bool _isJustStarted;

        public void OnMouseButtonPressedHandler(Vector3 startMousePosition)
        {
            if (_tilePrefab == null)
            {
                return;
            }

            _startPosition = startMousePosition.ToVector3Int();
            _lastPosition = startMousePosition.ToVector3Int();
            _isJustStarted = true;
        }

        //Ghosting tiles placement
        public void OnMouseMoveHandler(Vector3 lastMousePosition)
        {
            if (_tilePrefab == null)
            {
                return;
            }

            if (_lastPosition == lastMousePosition.ToVector3Int())
            {
                return;
            }

            _lastPosition = lastMousePosition.ToVector3Int();

            Rectangle ceilRectangle = new(_startPosition, _lastPosition);

            GameObject ghostTilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(GhostTileAssetPath);

            //Remove previous ghost tiles
            //In case if they exists
            if (_isJustStarted)
            {
                _isJustStarted = false;
            }
            else
            {
                Undo.PerformUndo();
            }

            _sceneEditor.PlaceTiles(ceilRectangle, ghostTilePrefab);
        }

        //Actual tile placement
        public void OnMouseButtonReleasedHandler(Vector3 lastMousePosition)
        {
            if (_tilePrefab == null)
            {
                return;
            }

            //Remove left ghosted tiles
            if (_isJustStarted == false)
            {
                Undo.PerformUndo();
            }

            Rectangle ceilRectangle = new(_lastPosition, _startPosition);
            _sceneEditor.PlaceTiles(ceilRectangle, _tilePrefab);
        }

        public void SetPrefabObject(GameObject prefab) => _tilePrefab = prefab;
    }
}