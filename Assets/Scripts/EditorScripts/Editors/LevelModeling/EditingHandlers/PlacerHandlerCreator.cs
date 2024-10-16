using System;
using AncientGlyph.GameScripts.ForEditor;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.EditingHandlers
{
    public static class PlacerHandlerCreator
    {
        public static IAssetPlacerHandler CreatePlacerHandler(AssetType typeAsset)
        {
            return typeAsset switch
            {
                AssetType.Tile => new TilePlacerHandler(),
                AssetType.Wall => new WallPlacerHandler(),
                AssetType.Entity => new CreaturePlacerHandler(),
                AssetType.Item => new ItemPlacerHandler(),
                _ => throw new ArgumentException(nameof(typeAsset) + "type cannot be instantiated")
            };
        }
    }
}