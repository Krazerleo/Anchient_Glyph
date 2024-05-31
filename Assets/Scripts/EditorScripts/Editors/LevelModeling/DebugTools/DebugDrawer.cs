using AncientGlyph.EditorScripts.Constants;
using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.DebugTools
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
                DrawWalls(cell, flatIndex);
                flatIndex++;
            }
        }

        public static void RemovePreviousDebugView()
        {
            GameObject[] debugGameObjects = GameObject.FindGameObjectsWithTag(DebugObjectTag);

            foreach (GameObject debugObject in debugGameObjects)
            {
                Object.DestroyImmediate(debugObject);
            }
        }

        private static void DrawWalls(CellModel cell, int flatIndex)
        {
            GameObject wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(WallDebugPrefabPath);
            GameObject floorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(FloorDebugPrefabPath);

            (int x, int y, int z) = ArrayExtensions.Get3dArrayIndex(
                flatIndex, 
                GameConstants.LevelCellsSizeX,
                GameConstants.LevelCellsSizeZ);

            for (int i = 0; i < 4; i++)
            {
                if (cell.GetWalls[i] != 0)
                {
                    var debugObject = Object.Instantiate(
                        wallPrefab,
                        position: new Vector3(x, y * EditorConstants.FloorHeight, z),
                        rotation: Quaternion.identity,
                        null);

                    debugObject.tag = DebugObjectTag;
                    debugObject.transform.GetChild(0).Rotate(Vector3.up, -90f * i, Space.World);
                }
            }

            if (cell.HasFloor)
            {
                Object.Instantiate(
                        floorPrefab,
                        position: new Vector3(x, y * EditorConstants.FloorHeight, z),
                        rotation: Quaternion.identity)
                    .tag = DebugObjectTag;
            }
        }
    }
}