using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing
{
    public class LevelModelDatabase
    {
        public static LevelModel LevelModelInstance;
        public static void Init(LevelModel levelModel)
        {
            LevelModelInstance = levelModel;
        }
    }
}