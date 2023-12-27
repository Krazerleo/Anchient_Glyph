using System;

using AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Helpers;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.DebugTools
{
    public class RedrawFromLevelModelTool
    {
        private const string wallDebugPrefabPath = "Assets/Level/Prefab/Debug/wall_debug.prefab";
        private const string floorDebugPrefabPath = "Assets/Level/Prefab/Debug/floor_debug.prefab";
        private const string debugObjectTag = "EditorDebugViewObject";

        [MenuItem("Project Instruments / Redraw Debug Level Model")]
        public static void RedrawLevelModel()
        {
            RemovePreviousDebugView();

            var levelModel = LevelModelDatabase.LevelModelInstance;

            int flatIndex = 0;
            foreach (var cell in levelModel)
            {
                DrawWalls(cell.GetWalls, flatIndex);
                flatIndex++;
            }
        }

        private static void RemovePreviousDebugView()
        {
            var debugGameObjects = GameObject.FindGameObjectsWithTag(debugObjectTag);
            foreach (var debugObject in debugGameObjects)
            {
                GameObject.DestroyImmediate(debugObject);
            }
        }

        private static void DrawWalls(Span<uint> walls, int flatIndex)
        {
            var (x, y, z) = ArrayTools.Get3dArrayIndex(flatIndex, GameConstants.LevelCellsSizeX, GameConstants.LevelCellsSizeY, GameConstants.LevelCellsSizeZ);

            for (int i = 0; i < 4; i++)
            {

            }
        }
    }
}