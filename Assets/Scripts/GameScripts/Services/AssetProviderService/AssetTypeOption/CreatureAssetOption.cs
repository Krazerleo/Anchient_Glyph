using System.Collections.Generic;

namespace AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption
{
    public struct CreatureAssetOption : IAssetTypeOption
    {
        // ReSharper disable once InconsistentNaming
        private static readonly IEnumerable<string> _labels
            = new List<string> { "creature" };

        public IEnumerable<object> Labels
            => _labels;
    }
}