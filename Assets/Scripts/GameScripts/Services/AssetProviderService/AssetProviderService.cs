#nullable enable
using System.Collections.Generic;
using AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace AncientGlyph.GameScripts.Services.AssetProviderService
{
    public class AssetProviderService<TAssetTypeOption>
        : IAssetProviderService<TAssetTypeOption>
        where TAssetTypeOption : IAssetTypeOption
    {
        private readonly ILoggingService _loggingService;
        private Dictionary<string, GameObject>? _resources;

        public AssetProviderService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async UniTask<GameObject?> GetAssetByName(string name)
        {
            await GetAllAssets();

            if (_resources!.TryGetValue(name, out GameObject? asset))
            {
                return asset;
            }
            
            _loggingService.LogError($"Unexpected asset name: {name}." +
                                     $"Cannot find in resources");
            
            return null;
        }

        private async UniTask GetAllAssets()
        {
            if (_resources != null)
            {
                return;
            }

            _resources = new Dictionary<string, GameObject>();

            AsyncOperationHandle<IList<IResourceLocation>> resourceLocationsHandle
                = Addressables.LoadResourceLocationsAsync(default(TAssetTypeOption)!.Labels,
                                                          Addressables.MergeMode.Union);

            IList<IResourceLocation> resourceLocations = await resourceLocationsHandle;

            if (resourceLocations.Count == 0)
            {
                _loggingService.LogError("Cannot get asset locations");
            }

            foreach (IResourceLocation resourceLocation in resourceLocations)
            {
                GameObject resource = await Addressables.LoadAssetAsync<GameObject>(resourceLocation);
                _resources.Add(resource.name, resource);
            }

            Addressables.Release(resourceLocationsHandle);
        }
    }
}