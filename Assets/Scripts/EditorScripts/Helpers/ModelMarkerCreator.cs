using AncientGlyph.EditorScripts.Editors;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.ForEditor.ModelMarkerComponent;
using AncientGlyph.GameScripts.Geometry.Shapes;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Helpers
{
    public class ModelMarkerCreator
    {
        public static void AddTileMarker(Vector3Int coordinates, GameObject gameObject)
        {
            var marker = gameObject.AddComponent<ModelMarker>();
            marker.Coordinates = coordinates;
            marker.DeletionHandler = new();
            marker.DeletionHandler += OnModelTileDelete;
        }

        public static void AddWallMarker(Vector3Int coordinates, Direction direction, GameObject gameObject)
        {
            var marker = gameObject.AddComponent<ModelMarker>();
            marker.Coordinates = coordinates;
            marker.Direction = direction;
            marker.DeletionHandler = new();
            marker.DeletionHandler += OnModelWallDelete;
        }

        public static void AddEntityMarker(Vector3Int coordinates, string entityId, GameObject gameObject)
        {
            var marker = gameObject.AddComponent<ModelMarker>();
            marker.Coordinates = coordinates;
            marker.EntityId = entityId;
            marker.DeletionHandler = new();
            marker.DeletionHandler += OnModelEntityDelete;
        }

        private static void OnModelTileDelete(ModelMarker modelMarker)
        {
            var levelEditor = new LevelModelEditor();
            levelEditor.RemoveTiles(new Point(modelMarker.Coordinates));
        }

        private static void OnModelWallDelete(ModelMarker modelMarker)
        {
            var levelEditor = new LevelModelEditor();
            levelEditor.RemoveWall(new Point(modelMarker.Coordinates), modelMarker.Direction);
        }

        private static void OnModelEntityDelete(ModelMarker modelMarker)
        {
            var levelEditor = new LevelModelEditor();
            levelEditor.RemoveEntity(new Point(modelMarker.Coordinates), modelMarker.EntityId);
        }
    }
}