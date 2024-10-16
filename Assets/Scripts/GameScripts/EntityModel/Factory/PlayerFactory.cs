using System;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Controller.PlayerControllerComponents;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.InputSystem;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption;
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
        private readonly IAssetProviderService<CreatureAssetOption> _creatureAssetProvider;
        private readonly ISaveDataService _saveDataService;
        private readonly GameInput _input;
        
        public PlayerFactory(LevelModel levelModel, ILoggingService loggingService,
                             IAssetProviderService<CreatureAssetOption> creatureAssetProvider,
                             GameInput input, ISaveDataService saveDataService)
        {
            _levelModel = levelModel;
            _loggingService = loggingService;
            _creatureAssetProvider = creatureAssetProvider;
            _input = input;
            _saveDataService = saveDataService;
        }

        public async UniTask<PlayerController> CreatePlayer()
        {
            GameObject playerAsset = await _creatureAssetProvider
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

            playerPrefab.transform.position = _saveDataService.BaseInfo.PlayerPosition;
            PlayerModel playerModel = new(_saveDataService.BaseInfo.PlayerPosition);

            MoveComponent moveComponent = new(_input.MoveInput, animator, _levelModel, playerModel, _loggingService);
            ActionComponent actionComponent = new(_input.ActionInput);
            return new PlayerController(playerModel, moveComponent, actionComponent);
        }
    }
}