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

        public void PlaceWall(IShape shape, GameObject wallPrefab, Direction direction)
        {
            if (wallPrefab == null)
            {
                return;
            }

            Undo.SetCurrentGroupName("Remove Walls");
            int groupId = Undo.GetCurrentGroup();

            var rotation = Quaternion.AngleAxis(90f * (1 + (uint) direction), Vector3.up);

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                Undo.RegisterCreatedObjectUndo(Object.Instantiate(wallPrefab, cellCoordinates, rotation * wallPrefab.transform.rotation, null), "");
            }

            Undo.CollapseUndoOperations(groupId);
        }

        #endregion Public Methods
    }
}