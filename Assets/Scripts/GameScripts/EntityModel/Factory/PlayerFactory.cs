using System;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Factory._Interfaces;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.PlayerInput;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;
using AncientGlyph.GameScripts.Services.LoggingService;
using AncientGlyph.GameScripts.Services.SaveDataService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

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

            var playerPrefab = await _creatureAssetProvider
                .GetAssetByName(GameConstants.PlayerName);

            if (playerPrefab.TryGetComponent<PlayerAnimator>(out var animator) == false)
            {
                const string message = "Player Animator not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            if (playerPrefab.TryGetComponent<PlayerMoveInput>(out var moveInput) == false)
            {
                const string message = "Player Move Input not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            playerPrefab.transform.position = _saveDataService.BaseInfo.PlayerPosition;
            var playerModel = new PlayerModel(_saveDataService.BaseInfo.PlayerPosition);

            return new PlayerController(animator, moveInput, _levelModel,
                                        playerModel, _loggingService);
        }
    }
}