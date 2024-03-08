using System.IO;

using AncientGlyph.GameScripts.Constants;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientGlyph.GameScripts.Serialization
{
    public static class LevelModelPathProvider
    {
        public static string GetPathFromRuntime()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtension);

            return levelModelPath;
        }
    }
}