#nullable enable
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    [UsedImplicitly]
    public class ItemFactory : IItemFactory
    {
        private readonly IAssetProviderService<ItemAssetOption> _itemAssetProvider;
        private readonly ILoggingService _loggingService;
        
        public ItemFactory(IAssetProviderService<ItemAssetOption> itemAssetProvider, ILoggingService loggingService)
        {
            _itemAssetProvider = itemAssetProvider;
            _loggingService = loggingService;
        }
        
        public async UniTask<GameItemView?> CreateGameItem(ItemSerializationInfo serializationInfo)
        {
            GameObject? itemAsset = await _itemAssetProvider.GetAssetByName(serializationInfo.Uid);

            if (itemAsset == null)
            {
                _loggingService.LogFatal($"There is no asset of item with name {serializationInfo.Uid}");
                return null;
            }

            GameObject itemPrefab = Object.Instantiate(itemAsset, serializationInfo.Position, Quaternion.identity);

            if (itemPrefab.TryGetComponent(out GameItemView itemView) == false)
            {
                _loggingService.LogFatal($"Item Config of item with id {serializationInfo.Uid} not found");
                return null;
            }

            return itemView;
        }
    }
}