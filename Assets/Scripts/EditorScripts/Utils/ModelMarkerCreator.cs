using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Utils
{
    public static class ModelMarkerCreator
    {
        public static void AddTileMarker(Vector3Int coordinates, GameObject toTileMarker)
        {
            var marker = toTileMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Tile;
            marker.Coordinates = coordinates;
        }

        public static void AddWallMarker(Vector3Int coordinates, Direction direction,
            GameObject toWallMarker)
        {
            var marker = toWallMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Wall;
            marker.Coordinates = coordinates;
            marker.Direction = direction;
        }

        public static void AddEntityMarker(Vector3Int coordinates, CreatureModel creatureModel,
            GameObject toEntityMarker)
        {
            var marker = toEntityMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Entity;
            marker.Coordinates = coordinates;
            marker.CreatureModel = creatureModel;
        }

        public static void AddItemMarker(Vector3 coordinates, GameItem gameItem, GameObject toItemMarker)
        {
            var marker = toItemMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Item;
            marker.ItemCoordinates = coordinates;
            marker.GameItem = gameItem;
        }
    }
}