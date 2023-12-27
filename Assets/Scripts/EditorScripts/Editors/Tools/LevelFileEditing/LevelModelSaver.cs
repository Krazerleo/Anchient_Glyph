using System.IO;
using System.Xml.Serialization;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing
{
    [InitializeOnLoad]
    public class LevelModelSaver
    {
        #region Private Methods

        static LevelModelSaver()
        {
            EditorApplication.quitting += SaveLevelModel;
        }

        [MenuItem("Project Instruments / Save Level Model")]
        private static void SaveLevelModel()
        {
            var currentSceneName = EditorSceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtention);

            if (File.Exists(levelModelPath))
            {
                using var writer = new StreamWriter(levelModelPath, false);
                var serializer = new XmlSerializer(typeof(LevelModel));

                serializer.Serialize(writer, LevelModelDatabase.LevelModelInstance);
            }
            else
            {
                Debug.LogWarning("Create Level Model Before Saving");
            }
        }

        #endregion Private Methods
    }
}