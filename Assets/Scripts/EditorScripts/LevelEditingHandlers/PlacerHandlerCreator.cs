using AncientGlyph.EditorScripts.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.GameWorldModel;


namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class PlacerHandlerCreator
    {
        public static IAssetPlacerHandler CreatePlacerHandler(AssetType typeAsset, LevelModel levelModel)
        {
            return typeAsset switch
            {
                AssetType.Tile or AssetType.Object or AssetType.Creature => new TilePlacerHandler(levelModel, null),
                AssetType.Wall => new WallPlacerHandler(levelModel, null),
                _ => throw new System.ArgumentException(),
            };
        }
    }
}