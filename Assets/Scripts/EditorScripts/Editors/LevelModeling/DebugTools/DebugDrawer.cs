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
            
            var levelModel = LevelModelLoader.GetOrCreateLevelModel();
            var flatIndex = 0;

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
                Object.DestroyImmediate(debugObject);
            }
        }

        private static void DrawWalls(CellModel cell, int flatIndex)
        {
            var wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(WallDebugPrefabPath);
            var floorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(FloorDebugPrefabPath);

            var (x, y, z) = ArrayExtensions.Get3dArrayIndex(flatIndex, GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeZ);

            for (var i = 0; i < 4; i++)
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