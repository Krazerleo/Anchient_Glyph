using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class PlacerHandlerCreator
    {
        public static IAssetPlacerHandler CreatePlacerHandler(AssetType typeAsset, LevelModel level)
        {
            return typeAsset switch
            {
                AssetType.Tile or AssetType.Object or AssetType.Creature => new TilePlacerHandler(level, null),
                AssetType.Wall => new WallPlacerHandler(),
                _ => throw new System.ArgumentException(),
            };
        }
    }
}