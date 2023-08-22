using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class LevelEditingByMouseHandlerCreator
    {
        public static ILevelEditingByMouseHandler CreateLevelEditingHandler(TypeAsset typeAsset, LevelModel level)
        {
            return typeAsset switch
            {
                TypeAsset.Tile or TypeAsset.Object or TypeAsset.Item => new TileEditorHandler(level),
                TypeAsset.Wall => new WallEditorHandler(),
                _ => throw new System.ArgumentException(),
            };
        }
    }
}