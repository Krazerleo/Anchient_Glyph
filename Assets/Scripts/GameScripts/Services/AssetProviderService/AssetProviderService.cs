using System;
using System.Collections.Generic;

using AncientGlyph.GameScripts.Services.LoggingService;

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AncientGlyph.GameScripts.Services.AssetProviderService
{
    public class AssetProviderService<TAssetTypeOption>
        : IAssetProviderService<TAssetTypeOption>
        where TAssetTypeOption : IAssetTypeOption
    {
        private readonly ILoggingService _loggingService;
        private Dictionary<string, GameObject> _resources;

        public AssetProviderService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async UniTask<GameObject> GetAssetByName(string name)
        {
            await GetAllAssets();

            if (_resources.TryGetValue(name, out var asset))
            {
                return UnityEngine.Object.Instantiate(asset);
            }
            else
            {
                var message = $"Unexpected asset name: {name}." +
                    $"Cannot find in resources";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }
        }

        private async UniTask GetAllAssets()
        {
            if (_resources != null)
            {
                return;
            }

            _resources = new();

            var resourceLocationsHandle = Addressables
                .LoadResourceLocationsAsync(default(TAssetTypeOption)!.Labels,
                                            Addressables.MergeMode.Union);

            var resourceLocations = await resourceLocationsHandle;

            if (resourceLocations.Count == 0)
            {
                _loggingService.LogError("Cannot get creature asset locations");
            }

            foreach (var resourceLocation in resourceLocations)
            {
                var resource = await Addressables.LoadAssetAsync<GameObject>(resourceLocation);
                _resources.Add(resource.name, resource);
            }

            Addressables.Release(resourceLocationsHandle);

            return;
        }
    }
}