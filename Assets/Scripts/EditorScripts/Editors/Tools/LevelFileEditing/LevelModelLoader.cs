using System.IO;
using System.Xml.Serialization;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing
{
    public class LevelModelLoader
    {
        #region Private Methods

        [MenuItem("Project Instruments / Get Or Create Level Model")]
        private static void GetOrCreateLevelModel()
        {
            var currentSceneName = EditorSceneManager.GetActiveScene().name;
            var streamingAssetsLevelFolderPath = Application.streamingAssetsPath;
            var levelModelPath = Path.Combine(streamingAssetsLevelFolderPath,
                                            FileConstants.StreamingAssetLevelFolderName,
                                            currentSceneName + FileConstants.LevelModelFileExtention);

            if (File.Exists(levelModelPath))
            {
                using var reader = File.OpenRead(levelModelPath);
                var deserializer = new XmlSerializer(typeof(LevelModel));

                LevelModelDatabase.Init((LevelModel) deserializer.Deserialize(reader));
            }
            else
            {
                Debug.Log("Level Model Creating Started");

                using var writer = File.OpenWrite(levelModelPath);
                var serializer = new XmlSerializer(typeof(LevelModel));

                var levelModel = new LevelModel();
                LevelModelDatabase.Init(levelModel);

                serializer.Serialize(writer, levelModel);

                Debug.Log("Level Model Creating Finished");
            }
        }

        #endregion Private Methods
    }
}