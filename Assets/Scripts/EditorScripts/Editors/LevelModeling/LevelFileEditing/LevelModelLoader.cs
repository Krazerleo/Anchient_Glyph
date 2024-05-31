//#define PRINT_DEBUG_DESERIALIZATION

using System.IO;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing
{
    public static class LevelModelLoader
    {
        public static LevelModel GetOrCreateLevelModel()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            string levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                FileConstants.StreamingAssetLevelFolderName,
                currentSceneName + FileConstants.LevelModelFileExtension);

            if (File.Exists(levelModelPath))
            {
#if PRINT_DEBUG_DESERIALIZATION
                Debug.Log($"Deserializing level model from {levelModelPath}");
#endif
                LevelDeserializer levelModelDeserializer = new(levelModelPath);
                
                if (levelModelDeserializer.TryDeserialize(out LevelModel levelModel, out _))
                {
#if PRINT_DEBUG_DESERIALIZATION
                    Debug.Log("Level model deserialized successfully");
#endif
                    return levelModel;
                }

                Debug.LogError("Level model deserialized unsuccessfully");
                return null;
            }

            Debug.LogError("Cannot find level model");
            return null;
        }
    }
}