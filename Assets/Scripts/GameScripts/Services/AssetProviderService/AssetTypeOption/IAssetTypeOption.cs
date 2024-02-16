using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Services.AssetProviderService
{
    public interface IAssetTypeOption
    {
        public IEnumerable<object> Labels { get; }
    }
}