using System;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.Interactors.Entities.Factory._Interfaces;
using AncientGlyph.GameScripts.Services.AssetProviderService;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Factory
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
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async UniTask<CreatureController>
            CreateCreature(Vector3Int position, CreatureModel creatureModel)
        {
            if (creatureModel == null)
            {
                const string message = "Creature Model is null";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            var creaturePrefab = await _creatureAssetProvider
                .GetAssetByName(creatureModel.SerializationName);

            creaturePrefab.transform.position = position;

            if (creaturePrefab.TryGetComponent<CreatureAnimator>(out var animator) == false)
            {
                var message = $"Animator of creature {creatureModel.SerializationName} not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            if (creaturePrefab.TryGetComponent<CreatureTraitsSource>(out var traitsSource) == false)
            {
                var message = $"Traits Source of creature {creatureModel.SerializationName} not found";
                _loggingService.LogError(message);
                throw new ArgumentException(message);
            }

            creatureModel.Traits = traitsSource.CreatureTraits;

            return new CreatureController(creatureModel, _levelModel, animator,
                CreatureBehaviour.CreateFromOptions(traitsSource.CreatureTraits.MovementType));
        }
    }
}