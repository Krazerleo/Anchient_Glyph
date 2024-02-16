using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Services.AssetProviderService
{
    public struct CreatureAssetOption : IAssetTypeOption
    {
        private static readonly IEnumerable<string> _labels
            = new List<string> { "creature" };

        public IEnumerable<object> Labels
            => _labels;
    }
}