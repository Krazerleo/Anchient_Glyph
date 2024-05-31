#nullable enable
using System;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.PlayerInput;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;
using AncientGlyph.GameScripts.Services.LoggingService;
using AncientGlyph.GameScripts.Services.SaveDataService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AncientGlyph.GameScripts.EntityModel.Factory
{
    [UsedImplicitly]
    public class PlayerFactory : IPlayerFactory
    {
        private readonly LevelModel _levelModel;
        private readonly ILoggingService _loggingService;
        private readonly IComponentLocatorService _componentLocator;
        private readonly IAssetProviderService<CreatureAssetOption> _creatureAssetProvider;
        private readonly ISaveDataService _saveDataService;
        
        public PlayerFactory(LevelModel levelModel, ILoggingService loggingService,
                             IAssetProviderService<CreatureAssetOption> creatureAssetProvider,
                             IComponentLocatorService componentLocator, ISaveDataService saveDataService)
        {
            _levelModel = levelModel;
            _loggingService = loggingService;
            _creatureAssetProvider = creatureAssetProvider;
            _componentLocator = componentLocator;
            _saveDataService = saveDataService;
        }

        public async UniTask<PlayerController> CreatePlayer()
        {
            if (_componentLocator.FindComponent<PlayerMoveInput>())
            {
                const string message = "Trying to duplicate player on scene";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            GameObject? playerAsset = await _creatureAssetProvider
                .GetAssetByName(GameConstants.PlayerName);

            if (playerAsset == null)
            {
                _loggingService.LogFatal("Cannot find player asset");
                throw new ArgumentException("FATAL ERROR: Player asset not found");
            }

            GameObject playerPrefab = Object.Instantiate(playerAsset, _saveDataService.BaseInfo.PlayerPosition,
                                                         Quaternion.identity);
            
            if (playerPrefab.TryGetComponent(out PlayerAnimator animator) == false)
            {
                const string message = "Player Animator not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            if (playerPrefab.TryGetComponent(out PlayerMoveInput moveInput) == false)
            {
                const string message = "Player Move Input not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            playerPrefab.transform.position = _saveDataService.BaseInfo.PlayerPosition;
            PlayerModel playerModel = new(_saveDataService.BaseInfo.PlayerPosition);

            return new PlayerController(animator, moveInput, _levelModel,
                                        playerModel, _loggingService);
        }
    }
}