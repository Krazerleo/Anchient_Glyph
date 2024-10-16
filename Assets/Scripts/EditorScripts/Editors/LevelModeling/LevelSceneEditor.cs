using AncientGlyph.GameScripts.EntityModel;
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
        private GameObject _itemMarkerPrefab;
        private const string ItemMarkerPrefabPath = "Assets/Level/Prefab/Debug/item_marker.prefab";

        private const string TilesGroupName = "{GROUND}";
        private const string WallsGroupName = "{WALLS}";
        private const string EntitiesGroupName = "[ENTITIES]";
        private const string ItemsGroupName = "{ITEMS}";

        public void PlaceTiles(IShape3D shape, GameObject tilePrefab)
        {
            if (tilePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Tiles");
            int undoGroupIndex = Undo.GetCurrentGroup();

            GameObject tilesGroup = GameObject.Find(TilesGroupName);
            foreach (Vector3Int cellCoordinates in shape.GetDefinedGeometry())
            {
                Vector3 heightAdjustedCoordinates = cellCoordinates;
                heightAdjustedCoordinates.y *= 1.5f;
                GameObject gameObject = Object.Instantiate(tilePrefab, heightAdjustedCoordinates, Quaternion.identity,
                                                           tilesGroup.transform);
                ModelMarkerCreator.AddTileMarker(cellCoordinates, gameObject);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(undoGroupIndex);
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
            int undoGroupIndex = Undo.GetCurrentGroup();

            _entityMarkerPrefab ??= AssetDatabase
                .LoadAssetAtPath<GameObject>(EntityMarkerPrefabPath);

            GameObject entitiesGroup = GameObject.Find(EntitiesGroupName);
            foreach (Vector3Int cellCoordinates in shape.GetDefinedGeometry())
            {
                Vector3 heightAdjustedCoordinates = cellCoordinates;
                heightAdjustedCoordinates.y *= 1.5f;
                
                GameObject entityMarker = Object.Instantiate(_entityMarkerPrefab, heightAdjustedCoordinates,
                                                             Quaternion.identity, entitiesGroup.transform);
                
                CreatureModel creatureModel = new(traitSource.CreatureTraits, creaturePrefab.name, cellCoordinates);
                ModelMarkerCreator.AddEntityMarker(cellCoordinates, creatureModel, entityMarker);
                Undo.RegisterCreatedObjectUndo(entityMarker, "");
            }

            Undo.CollapseUndoOperations(undoGroupIndex);
        }

        public void PlaceWalls(IShape3D shape, GameObject wallPrefab, Direction direction)
        {
            if (wallPrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Walls");
            int undoGroupIndex = Undo.GetCurrentGroup();
            GameObject wallsGroup = GameObject.Find(WallsGroupName);

            foreach (Vector3Int cellCoordinates in shape.GetDefinedGeometry())
            {
                Vector3 heightAdjustedCoordinates = cellCoordinates;
                heightAdjustedCoordinates.y *= 1.5f;
                GameObject gameObject = Object.Instantiate(wallPrefab, heightAdjustedCoordinates, Quaternion.identity,
                                                           wallsGroup.transform);
                ModelMarkerCreator.AddWallMarker(cellCoordinates, direction, gameObject);
                gameObject.transform.GetChild(0).Rotate(Vector3.up, -90f * (uint)direction, Space.World);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(undoGroupIndex);
        }

        public void PlaceItem(Vector3 itemPosition, GameObject itemPrefab)
        {
            if (itemPrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Item");
            int undoGroupIndex = Undo.GetCurrentGroup();
            GameObject itemGroup = GameObject.Find(ItemsGroupName);

            _itemMarkerPrefab ??= AssetDatabase
                .LoadAssetAtPath<GameObject>(ItemMarkerPrefabPath);
            
            GameObject itemMarker = Object.Instantiate(_itemMarkerPrefab, itemPosition,
                                                       Quaternion.identity, itemGroup.transform);
            
            ModelMarkerCreator.AddItemMarker(itemPosition, itemPrefab.name, itemMarker);

            Undo.RegisterCreatedObjectUndo(itemMarker, "");
            Undo.CollapseUndoOperations(undoGroupIndex);
        }
    }
}