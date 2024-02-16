using AncientGlyph.EditorScripts.Helpers;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelSceneEditor
    {
        private GameObject _entityMarkerPrefab;
        private const string _entityMarkerPrefabPath = "Assets/Level/Prefab/Debug/entity_marker.prefab";

        private const string TilesGroupName = "{GROUND}";
        private const string WallsGroupName = "{WALLS}";
        private const string EntitiesGroupName = "[ENTITIES]";

        public void PlaceTiles(IShape3D shape, GameObject tilePrefab)
        {
            if (tilePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Tiles");
            int groupId = Undo.GetCurrentGroup();

            var tilesGroup = GameObject.Find(TilesGroupName);

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                var gameObject = Object.Instantiate(tilePrefab, cellCoordinates, Quaternion.identity, tilesGroup.transform);
                ModelMarkerCreator.AddTileMarker(cellCoordinates, gameObject);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        public void PlaceEntities(IShape3D shape, GameObject creaturePrefab, string entityId)
        {
            if (creaturePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Creature");
            int groupId = Undo.GetCurrentGroup();

            _entityMarkerPrefab ??= AssetDatabase
                .LoadAssetAtPath<GameObject>(_entityMarkerPrefabPath);

            var entitiesGroup = GameObject.Find(EntitiesGroupName);

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                Object.Instantiate(_entityMarkerPrefab, cellCoordinates, Quaternion.identity, entitiesGroup.transform);
                ModelMarkerCreator.AddEntityMarker(cellCoordinates, entityId, _entityMarkerPrefab);
                Undo.RegisterCreatedObjectUndo(_entityMarkerPrefab, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        public void PlaceWalls(IShape3D shape, GameObject wallPrefab, Direction direction)
        {
            if (wallPrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Walls");
            int groupId = Undo.GetCurrentGroup();

            var wallsGroup = GameObject.Find(WallsGroupName);

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                var gameObject = Object.Instantiate(wallPrefab, cellCoordinates, Quaternion.identity, wallsGroup.transform);
                ModelMarkerCreator.AddWallMarker(cellCoordinates, direction, gameObject);
                gameObject.transform.GetChild(0).Rotate(Vector3.up, -90f * (uint) direction, Space.World);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }
    }
}