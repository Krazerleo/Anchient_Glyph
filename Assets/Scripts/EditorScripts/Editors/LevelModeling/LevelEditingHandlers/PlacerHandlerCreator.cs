using System;

using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.ForEditor;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers
{
    public class PlacerHandlerCreator
    {
        public static IAssetPlacerHandler CreatePlacerHandler(AssetType typeAsset)
        {
            return typeAsset switch
            {
                AssetType.Tile => new TilePlacerHandler(),
                AssetType.Wall => new WallPlacerHandler(),
                AssetType.Entity => new CreaturePlacerHandler(),
                AssetType.Item => new CreaturePlacerHandler(),
                AssetType.Object => new ObjectPlacerHandler(),
                _ => throw new NotImplementedException()
            };
        }
    }
}