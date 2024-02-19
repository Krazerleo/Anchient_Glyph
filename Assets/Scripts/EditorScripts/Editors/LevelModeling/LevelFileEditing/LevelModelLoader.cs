//#define PRINT_LOG_DESERIALIZATION

using System.IO;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing
{
    [InitializeOnLoad]
    public class LevelModelLoader
    {
        static LevelModelLoader()
        {
            AssemblyReloadEvents.afterAssemblyReload += GetOrCreateLevelModel;
            SceneManager.sceneLoaded += OnSceneLoadedEvent;
        }

        private static void OnSceneLoadedEvent(Scene scene, LoadSceneMode mode)
            => GetOrCreateLevelModel();

        [MenuItem("Project Instruments / Get Or Create Level Model")]
        private static void GetOrCreateLevelModel()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtention);

            if (File.Exists(levelModelPath))
            {
#if PRINT_LOG_DESERIALIZATION
                Debug.Log($"Deserializing level model from {levelModelPath}");
#endif
                var levelModelDeserializer = new LevelModelDeserializer(levelModelPath);

                if (levelModelDeserializer.TryDeserialize(out var levelModel))
                {
                    LevelModelData.Init(levelModel);
#if PRINT_LOG_DESERIALIZATION
                    Debug.Log("Level model deserialized successfully");
#endif
                }
                else
                {
                    Debug.LogError("Level model deserialized unsuccessfully");
                }
            }
            else
            {
                Debug.Log("Level model creating started");

                var levelModel = new LevelModel();
                LevelModelData.Init(levelModel);

                var levelModelSerializer = new LevelModelSerializer(levelModelPath);
                levelModelSerializer.Serialize(levelModel);

                Debug.Log("Level model creating finished");
            }
        }
    }
}