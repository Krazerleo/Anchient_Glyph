using System.IO;
using System.Xml;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Serialization;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing
{
    [InitializeOnLoad]
    public class LevelModelSaver
    {
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
                Debug.Log("Started level model serialization");

                using var writer = new StreamWriter(levelModelPath, false);
                using var serializer = XmlWriter.Create(writer);
                var levelModelSerializer = new LevelModelSerializer(serializer);

                levelModelSerializer.Serialize(LevelModelData.GetLevelModel());

                Debug.Log("Finished level model serialization");
            }
            else
            {
                Debug.LogWarning("Create level model before saving");
            }
        }
    }
}