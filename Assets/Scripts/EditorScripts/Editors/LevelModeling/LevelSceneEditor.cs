using AncientGlyph.EditorScripts.Utils;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling
{
    public class LevelSceneEditor
    {
        private GameObject _entityMarkerPrefab;
        private const string EntityMarkerPrefabPath = "Assets/Level/Prefab/Debug/entity_marker.prefab";

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
                var heightAdjustedCoordinates = (Vector3)cellCoordinates;
                heightAdjustedCoordinates.y *= 1.5f;
                var gameObject = Object.Instantiate(tilePrefab, heightAdjustedCoordinates, Quaternion.identity, tilesGroup.transform);
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

            if (creaturePrefab.TryGetComponent<CreatureTraitsSource>(out var traitSource) == false)
            {
                Debug.Log($"Can`t find traits of this creature: {creaturePrefab.name}");
                return;
            }

            Undo.SetCurrentGroupName("Remove Creature");
            int groupId = Undo.GetCurrentGroup();

            _entityMarkerPrefab ??= AssetDatabase
                .LoadAssetAtPath<GameObject>(EntityMarkerPrefabPath);

            var entitiesGroup = GameObject.Find(EntitiesGroupName);

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                var heightAdjustedCoordinates = (Vector3)cellCoordinates;
                heightAdjustedCoordinates.y *= 1.5f;
                var entityMarker = Object.Instantiate(_entityMarkerPrefab, heightAdjustedCoordinates, Quaternion.identity, entitiesGroup.transform);
                var creatureModel = new CreatureModel(traitSource.CreatureTraits, creaturePrefab.name, cellCoordinates);
                ModelMarkerCreator.AddEntityMarker(cellCoordinates, creatureModel, entityMarker);
                Undo.RegisterCreatedObjectUndo(entityMarker, "");
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
                var heightAdjustedCoordinates = (Vector3)cellCoordinates;
                heightAdjustedCoordinates.y *= 1.5f;
                var gameObject = Object.Instantiate(wallPrefab, heightAdjustedCoordinates, Quaternion.identity, wallsGroup.transform);
                ModelMarkerCreator.AddWallMarker(cellCoordinates, direction, gameObject);
                gameObject.transform.GetChild(0).Rotate(Vector3.up, -90f * (uint) direction, Space.World);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }
    }
}