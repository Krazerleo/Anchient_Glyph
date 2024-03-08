using System;
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

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing
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
            var currentSceneName = SceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtension);

            var levelModel = new LevelModel();
            var levelEditor = new LevelModelEditor(levelModel);
            
            var markers = UnityEngine.Object.FindObjectsOfType<ModelMarker>();

            foreach (var marker in markers)
            {
                var coordinates = new Point(marker.Coordinates);
                
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
                    case AssetType.Object:
                        break;
                    case AssetType.Item:
                        break;
                    case AssetType.Entity:
                        levelEditor.TryPlaceEntity(coordinates, marker.CreatureModel);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var serializer = new LevelModelSerializer(levelModelPath);
            serializer.Serialize(levelModel);
        }
    }
}