using System.IO;

using AncientGlyph.GameScripts.Constants;

using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.GameScripts.Serialization
{
    public static class LevelModelPathProvider
    {
        public static string GetPathFromEditor()
        {
            var currentSceneName = EditorSceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtention);

            return levelModelPath;
        }

        public static string GetPathFromRuntime()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtention);

            return levelModelPath;
        }
    }
}