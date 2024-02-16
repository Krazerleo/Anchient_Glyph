//#define PRINT_LOG_SERIALIZATION

using System.IO;

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
            AssemblyReloadEvents.beforeAssemblyReload += SaveLevelModel;

            EditorSceneManager.sceneSaved += OnSceneSaved;
        }

        private static void OnSceneSaved(Scene scene)
            => SaveLevelModel();

        [MenuItem("Project Instruments / Save Level Model")]
        private static void SaveLevelModel()
        {
            var levelModelPath = LevelModelPathProvider.GetPathFromEditor();

            if (File.Exists(levelModelPath) && LevelModelData.GetLevelModel() != null)
            {
#if PRINT_LOG_SERIALIZATION
                Debug.Log("Started level model serialization");
#endif
                var levelModelSerializer = new LevelModelSerializer(levelModelPath);

                levelModelSerializer.Serialize(LevelModelData.GetLevelModel());
#if PRINT_LOG_SERIALIZATION
                Debug.Log("Finished level model serialization");
#endif
            }
            else
            {
                Debug.LogWarning("Create level model before saving");
            }
        }
    }
}