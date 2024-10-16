using AncientGlyph.EditorScripts.Constants;
using AncientGlyph.EditorScripts.Editors.LevelModeling.Serialialization;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.DebugUtilities
{
    public static class DebugDrawer
    {
        private const string WallDebugPrefabPath = "Assets/Level/Prefab/Debug/wall_debug.prefab";
        private const string FloorDebugPrefabPath = "Assets/Level/Prefab/Debug/floor_debug.prefab";
        private const string DebugObjectTag = "EditorDebugViewObject";

        public static void RedrawLevelModel()
        {
            RemovePreviousDebugView();
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            LevelModel levelModel = LevelModelLoader.GetOrCreateLevelModel();
            int flatIndex = 0;

            foreach (CellModel cell in levelModel)
            {
                DrawCells(cell, flatIndex);
                flatIndex++;
            }
        }

        public static void RemovePreviousDebugView()
        {
            foreach (GameObject debugObject in GameObject.FindGameObjectsWithTag(DebugObjectTag))
            {
                Object.DestroyImmediate(debugObject);
            }
        }

        private static void DrawCells(CellModel cell, int flatIndex)
        {
            GameObject wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(WallDebugPrefabPath);
            GameObject floorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(FloorDebugPrefabPath);

            (int x, int y, int z) = ArrayExtensions.Get3dArrayIndex(flatIndex,
                                                                    GameConstants.LevelCellsSizeX,
                                                                    GameConstants.LevelCellsSizeZ);

            for (int i = 0; i < 4; i++)
            {
                if (cell.GetWalls[i] == 0)
                {
                    continue;
                }

                Vector3 debugWallPosition = new(x * EditorConstants.FloorSizeX,
                                                y * EditorConstants.FloorHeight,
                                                z * EditorConstants.FloorSizeZ);
                GameObject debugObject = Object.Instantiate(wallPrefab,
                                                            position: debugWallPosition,
                                                            rotation: Quaternion.identity,
                                                            null);
                debugObject.tag = DebugObjectTag;
                debugObject.transform.GetChild(0).Rotate(Vector3.up, -90f * i, Space.World);
            }

            if (cell.HasFloor)
            {
                Vector3 debugFloorPosition = new(x * EditorConstants.FloorSizeX,
                                                 y * EditorConstants.FloorHeight,
                                                 z * EditorConstants.FloorSizeZ);
                Object.Instantiate(floorPrefab,
                                   position: debugFloorPosition,
                                   rotation: Quaternion.identity)
                      .tag = DebugObjectTag;
            }

            if (cell.HasCeil)
            {
                Vector3 debugCeilPosition = new(x * EditorConstants.FloorSizeX,
                                                (y + 1) * EditorConstants.FloorHeight,
                                                z * EditorConstants.FloorSizeZ);
                Object.Instantiate(floorPrefab,
                                   position: debugCeilPosition,
                                   rotation: Quaternion.identity)
                      .tag = DebugObjectTag;
            }
        }
    }
}