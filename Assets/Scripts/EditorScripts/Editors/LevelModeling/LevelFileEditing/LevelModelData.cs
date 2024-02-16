using AncientGlyph.GameScripts.GameWorldModel;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing
{
    public class LevelModelData
    {
        private static LevelModel _levelModelInstance;

        public static void Init(LevelModel levelModel)
        {
            _levelModelInstance = levelModel;
        }

        public static LevelModel GetLevelModel()
        {
            if (_levelModelInstance == null)
            {
                Debug.LogError("Level model instance is null.\n" +
                               "Try create new level model or load existing.");
            }

            return _levelModelInstance;
        }
    }
}