#nullable enable
using AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.AssetProviderService
{
    public interface IAssetProviderService<TAssetOption>
        where TAssetOption : IAssetTypeOption
    {
        public UniTask<GameObject?> GetAssetByName(string name);
    }
}