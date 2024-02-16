using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Services.AssetProviderService
{
    public struct ItemAssetOption : IAssetTypeOption
    {
        private static readonly IEnumerable<string> _labels
            = new List<string> { "item" };

        public IEnumerable<object> Labels
            => _labels;
    }
}