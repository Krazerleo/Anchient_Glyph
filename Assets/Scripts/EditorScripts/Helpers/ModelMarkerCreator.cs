using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Helpers
{
    public static class ModelMarkerCreator
    {
        public static void AddTileMarker(Vector3Int coordinates, GameObject tileMarker)
        {
            var marker = tileMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Tile;
            marker.Coordinates = coordinates;
        }

        public static void AddWallMarker(Vector3Int coordinates, Direction direction,
            GameObject wallMarker)
        {
            var marker = wallMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Wall;
            marker.Coordinates = coordinates;
            marker.Direction = direction;
        }

        public static void AddEntityMarker(Vector3Int coordinates, CreatureModel creatureModel,
            GameObject entityMarker)
        {
            var marker = entityMarker.AddComponent<ModelMarker>();
            marker.Type = AssetType.Entity;
            marker.Coordinates = coordinates;
            marker.CreatureModel = creatureModel;
        }
    }
}