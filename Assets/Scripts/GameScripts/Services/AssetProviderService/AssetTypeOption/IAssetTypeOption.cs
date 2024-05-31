using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption
{
    public interface IAssetTypeOption
    {
        public IEnumerable<object> Labels { get; }
    }
}