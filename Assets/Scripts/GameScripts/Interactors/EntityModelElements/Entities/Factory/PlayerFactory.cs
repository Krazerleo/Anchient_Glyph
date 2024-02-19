using System;

using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities.Controllers;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.LoggingService;
using AncientGlyph.GameScripts.PlayerInput;
using AncientGlyph.GameScripts.Services.ComponentLocatorService;

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly LevelModel _levelModel;
        private readonly ILoggingService _loggingService;
        private readonly IComponentLocatorService _componentLocator;
        private readonly IAssetProviderService<CreatureAssetOption> _creatureAssetProvider;

        public PlayerFactory(LevelModel levelModel,
                             ILoggingService loggingService,
                             IAssetProviderService<CreatureAssetOption> creatureAssetProvider,
                             IComponentLocatorService componentLocator)
        {
            _levelModel = levelModel;
            _loggingService = loggingService;
            _creatureAssetProvider = creatureAssetProvider;
            _componentLocator = componentLocator;
        }

        public async UniTask<PlayerController> CreatePlayer()
        {
            if (_componentLocator.FindComponent<PlayerMoveInput>())
            {
                var message = "Trying to duplicate player on scene";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            var playerPrefab = await _creatureAssetProvider
                .GetAssetByName(GameConstants.PlayerName);

            if (playerPrefab.TryGetComponent<PlayerAnimator>(out var animator) == false)
            {
                var message = "Player Animator not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            if (playerPrefab.TryGetComponent<PlayerMoveInput>(out var moveInput) == false)
            {
                var message = "Player Move Input not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            playerPrefab.transform.position = new Vector3Int(5, 5, 1);
            var playerModel = new PlayerModel();
            playerModel.Position = new Vector3Int(5, 5, 1);

            return new PlayerController(animator, moveInput, _levelModel,
                                        new PlayerModel(), _loggingService);
        }
    }
}