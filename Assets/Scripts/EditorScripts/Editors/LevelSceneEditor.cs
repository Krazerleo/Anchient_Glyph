using AncientGlyph.EditorScripts.Helpers;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelSceneEditor
    {
        public void PlaceTile(IShape3D shape, GameObject tilePrefab)
        {
            if (tilePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Tiles");
            int groupId = Undo.GetCurrentGroup();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                var gameObject = Object.Instantiate(tilePrefab, cellCoordinates, Quaternion.identity, null);
                ModelMarkerCreator.AddTileMarker(cellCoordinates, gameObject);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        public void PlaceEntity(IShape3D shape, GameObject creaturePrefab, string entityId)
        {
            if (creaturePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Creature");
            int groupId = Undo.GetCurrentGroup();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                var gameObject = Object.Instantiate(creaturePrefab, cellCoordinates, Quaternion.identity, null);
                ModelMarkerCreator.AddEntityMarker(cellCoordinates, entityId, gameObject);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        public void PlaceWall(IShape3D shape, GameObject wallPrefab, Direction direction)
        {
            if (wallPrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Walls");
            int groupId = Undo.GetCurrentGroup();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                var gameObject = Object.Instantiate(wallPrefab, cellCoordinates, Quaternion.identity, null);
                ModelMarkerCreator.AddWallMarker(cellCoordinates, direction, gameObject);
                gameObject.transform.GetChild(0).Rotate(Vector3.up, -90f * (uint) direction, Space.World);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }
    }
}