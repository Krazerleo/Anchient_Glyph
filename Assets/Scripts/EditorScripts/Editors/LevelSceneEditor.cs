using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelSceneEditor
    {
        #region Public Methods

        public void PlaceTile(IShape shape, GameObject tilePrefab)
        {
            if (tilePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Tiles");
            int groupId = Undo.GetCurrentGroup();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                Undo.RegisterCreatedObjectUndo(Object.Instantiate(tilePrefab, cellCoordinates, Quaternion.identity, null), "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        public void PlaceCreature(IShape shape, GameObject creaturePrefab)
        {
            if (creaturePrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Creature");
            int groupId = Undo.GetCurrentGroup();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                Undo.RegisterCreatedObjectUndo(Object.Instantiate(creaturePrefab, cellCoordinates, Quaternion.identity, null), "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        public void PlaceWall(IShape shape, GameObject wallPrefab, Direction direction)
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
                gameObject.transform.GetChild(0).Rotate(Vector3.up, 90f * (uint) direction, Space.World);
                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        #endregion Public Methods
    }
}