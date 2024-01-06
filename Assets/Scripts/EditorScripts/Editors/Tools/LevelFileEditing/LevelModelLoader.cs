using System.IO;
using System.Xml;
using System.Xml.Serialization;

using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Serialization;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing
{
    public class LevelModelLoader
    {
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
                Debug.Log($"Deserializing level model from {levelModelPath}");

                using var reader = File.OpenRead(levelModelPath);
                using var deserializer = XmlReader.Create(reader);
                var levelModelDeserializer = new LevelModelDeserializer(deserializer);

                if (levelModelDeserializer.TryDeserialize(out var levelModel))
                {
                    LevelModelData.Init(levelModel);
                    Debug.Log("Level model deserialized successfully");
                }
                else
                {
                    Debug.LogError("Level model deserialized unsuccessfully");
                }
            }
            else
            {
                Debug.Log("Level model creating started");

                using var writer = File.OpenWrite(levelModelPath);
                var serializer = new XmlSerializer(typeof(LevelModel));

                var levelModel = new LevelModel();
                LevelModelData.Init(levelModel);

                serializer.Serialize(writer, levelModel);

                Debug.Log("Level model creating finished");
            }
        }
    }
}