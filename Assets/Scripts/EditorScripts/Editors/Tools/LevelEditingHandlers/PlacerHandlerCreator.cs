using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class PlacerHandlerCreator
    {
        #region Public Methods

        public static IAssetPlacerHandler CreatePlacerHandler(AssetType typeAsset)
        {
            return typeAsset switch
            {
                AssetType.Tile => new TilePlacerHandler(),
                AssetType.Wall => new WallPlacerHandler(),
                AssetType.Creature => new CreaturePlacerHandler(),
                _ => throw new System.ArgumentException(),
            };
        }

        #endregion Public Methods
    }
}