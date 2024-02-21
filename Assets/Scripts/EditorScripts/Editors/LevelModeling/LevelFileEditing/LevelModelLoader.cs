//#define PRINT_LOG_DESERIALIZATION

using System.IO;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing
{
    public class LevelModelLoader
    {
        public static LevelModel GetOrCreateLevelModel()
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
#if PRINT_LOG_DESERIALIZATION
                    Debug.Log("Level model deserialized successfully");
#endif
                    return levelModel;
                }

                Debug.LogError("Level model deserialized unsuccessfully");
                return null;
            }
            else
            {
                Debug.LogError("Cannot find level model");
                return null;
            }
        }
    }
}