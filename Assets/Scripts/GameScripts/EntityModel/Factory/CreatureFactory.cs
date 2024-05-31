#nullable enable
using System;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.AssetProviderService.AssetTypeOption;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AncientGlyph.GameScripts.EntityModel.Factory
{
    [UsedImplicitly]
    public class CreatureFactory : ICreatureFactory
    {
        private readonly ILoggingService _loggingService;
        private readonly IAssetProviderService<CreatureAssetOption> _creatureAssetProvider;
        private readonly LevelModel _levelModel;

        public CreatureFactory(ILoggingService loggingService,
            IAssetProviderService<CreatureAssetOption> creatureAssetProvider,
            LevelModel levelModel)
        {
            _creatureAssetProvider = creatureAssetProvider;
            _loggingService = loggingService;
            _levelModel = levelModel;
        }

        /// <summary>
        /// Create creature
        /// </summary>
        /// <param name="position">Position where to place creature</param>
        /// <param name="creatureModel">Creature model (before configuration)</param>
        /// <param name="playerController">Player Controller</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async UniTask<CreatureController?>
            CreateCreature(Vector3Int position, CreatureModel creatureModel, PlayerController playerController)
        {
            if (creatureModel == null)
            {
                const string message = "Creature Model is null";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            GameObject? creatureAsset = await _creatureAssetProvider
                .GetAssetByName(creatureModel.SerializationName);

            if (creatureAsset == null)
            {
                _loggingService.LogFatal($"There is no asset of creature with name {creatureModel.SerializationName}");
                return null;
            }

            GameObject creaturePrefab = Object.Instantiate(creatureAsset);
            
            if (creaturePrefab.TryGetComponent(out CreatureAnimator animator) == false)
            {
                _loggingService.LogFatal($"Animator of creature {creatureModel.SerializationName} not found");
                return null;
            }

            if (creaturePrefab.TryGetComponent(out CreatureTraitsSource traitsSource) == false)
            {
                _loggingService.LogFatal($"Traits Source of creature {creatureModel.SerializationName} not found");
                return null;
            }

            creaturePrefab.transform.position = position;
            creatureModel.PostInitialize(traitsSource);
            
            return new CreatureController(creatureModel, playerController, _levelModel, animator,
                CreatureBehaviour.CreateFromOptions(traitsSource.CreatureTraits.MovementType, _levelModel),
                _loggingService);
        }
    }
}