using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.DebugTools
{
    public class DebugDrawer
    {
        private const string WallDebugPrefabPath = "Assets/Level/Prefab/Debug/wall_debug.prefab";
        private const string FloorDebugPrefabPath = "Assets/Level/Prefab/Debug/floor_debug.prefab";
        private const string DebugObjectTag = "EditorDebugViewObject";

        public static void RedrawLevelModel()
        {
            RemovePreviousDebugView();

            var levelModel = LevelModelData.GetLevelModel();

            int flatIndex = 0;
            foreach (var cell in levelModel)
            {
                DrawWalls(cell, flatIndex);
                flatIndex++;
            }
        }

        public static void RemovePreviousDebugView()
        {
            var debugGameObjects = GameObject.FindGameObjectsWithTag(DebugObjectTag);

            foreach (var debugObject in debugGameObjects)
            {
                GameObject.DestroyImmediate(debugObject);
            }
        }

        private static void DrawWalls(CellModel cell, int flatIndex)
        {
            var wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(WallDebugPrefabPath);
            var floorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(FloorDebugPrefabPath);

            var (x, y, z) = ArrayTools.Get3dArrayIndex(flatIndex, GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeZ);

            for (int i = 0; i < 4; i++)
            {
                if (cell.GetWalls[i] != 0)
                {
                    var debugObject = GameObject.Instantiate(
                        wallPrefab,
                        position: new Vector3(x, y, z),
                        rotation: Quaternion.identity,
                        null);

                    debugObject.tag = DebugObjectTag;
                    debugObject.transform.GetChild(0).Rotate(Vector3.up, -90f * i, Space.World);
                }
            }

            if (cell.HasFloor == true)
            {
                GameObject.Instantiate(
                    floorPrefab,
                    position: new Vector3(x, y, z),
                    rotation: Quaternion.identity)
                .tag = DebugObjectTag;
            }
        }
    }
}