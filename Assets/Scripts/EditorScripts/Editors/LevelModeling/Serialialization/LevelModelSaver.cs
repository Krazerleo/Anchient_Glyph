using System;
using System.Collections.Generic;
using System.IO;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Serialization;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.Serialialization
{
    [InitializeOnLoad]
    public class LevelModelSaver
    {
        static LevelModelSaver()
        {
            EditorApplication.quitting += SaveLevelModel;
            EditorSceneManager.sceneSaved += OnSceneSaved;
            AssemblyReloadEvents.beforeAssemblyReload += SaveLevelModel;
        }

        private static void OnSceneSaved(Scene scene)
            => SaveLevelModel();

        [MenuItem("Project Instruments / Save Level Model")]
        private static void SaveLevelModel()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            string levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtension);

            LevelModel levelModel = new();
            List<(string, Vector3)> gameItemsOnScene = new();
            
            LevelModelEditor levelEditor = new(levelModel);
            ModelMarker[] markers = UnityEngine.Object.FindObjectsOfType<ModelMarker>();

            foreach (ModelMarker marker in markers)
            {
                Point coordinates = new(marker.Coordinates);
                
                switch (marker.Type)
                {
                    case AssetType.None:
                        break;
                    case AssetType.Tile:
                        levelEditor.TryPlaceTile(coordinates);
                        break;
                    case AssetType.Wall:
                        levelEditor.TryPlaceWall(coordinates, marker.Direction);
                        break;
                    case AssetType.Item:
                        gameItemsOnScene.Add((marker.GameItemIdentifier, marker.ItemCoordinates));
                        break;
                    case AssetType.Entity:
                        levelEditor.TryPlaceEntity(coordinates, marker.CreatureModel);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            LevelSerializer serializer = new(levelModelPath);
            serializer.Serialize(levelModel, gameItemsOnScene);
        }
    }
}