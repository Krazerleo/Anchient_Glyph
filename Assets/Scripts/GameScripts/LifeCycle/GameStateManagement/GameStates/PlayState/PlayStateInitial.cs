#nullable enable
using System.Collections.Generic;
using AncientGlyph.GameScripts.CoreGameMechanics;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Factory;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagement.StateMachine;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates.PlayState
{
    [UsedImplicitly]
    public class PlayStateInitial : IGameState
    {
        private IGameStateMachine? _stateMachine;
        private readonly ICreatureFactory _creatureFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IItemFactory _itemFactory;
        private readonly LevelModel _levelModel;
        private readonly ItemsSerializationContainer _itemsContainer;
        private readonly GameLoop _gameLoop;
        private readonly ILoggingService _logger;

        public PlayStateInitial(ICreatureFactory creatureFactory,
            IPlayerFactory playerFactory, IItemFactory itemFactory,
            LevelModel levelModel, GameLoop gameLoop,
            ItemsSerializationContainer itemsContainer,
            ILoggingService logger)
        {
            _creatureFactory = creatureFactory;
            _playerFactory = playerFactory;
            _itemFactory = itemFactory;
            _levelModel = levelModel;
            _gameLoop = gameLoop;
            _itemsContainer = itemsContainer;
            _logger = logger;
        }

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            InjectItemsOnScene()
                .ContinueWith(InjectPlayerToGameLoop)
                .ContinueWith(InjectCreaturesToGameLoop)
                .ContinueWith(_gameLoop.StartGameLoop)
                .Forget();
        }

        public void Exit() { }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
            => _stateMachine = stateMachine;

        private async UniTask InjectItemsOnScene()
        {
            foreach (ItemSerializationInfo itemInfo in _itemsContainer.ItemData)
            {
                GameItemView? itemView = await _itemFactory.CreateGameItem(itemInfo);

                if (itemView == null)
                {
                    _logger.LogFatal($"Null item view with id {itemInfo.Uid}. Pass it away");
                }
            }
        }

        private async UniTask InjectCreaturesToGameLoop(PlayerController playerController)
        {
            IEnumerable<IEntityModel> entities = _levelModel.GetAllCurrentEntities();

            foreach (IEntityModel entity in entities)
            {
                if (entity is not CreatureModel creatureModel)
                {
                    _logger.LogError($"entity is not of type {nameof(CreatureModel)}");
                    return;
                }

                CreatureController? controller = await _creatureFactory
                    .CreateCreature(entity.Position, creatureModel, playerController);

                if (controller == null)
                {
                    _logger.LogError($"Null creature controller with id {creatureModel.SerializationName}." +
                                     $" Pass it away");
                    continue;
                }

                _gameLoop.InjectEntityController(controller);
            }
        }

        private async UniTask<PlayerController> InjectPlayerToGameLoop()
        {
            // _logger.LogDebug("Player injected");
            PlayerController player = await _playerFactory.CreatePlayer();

            _gameLoop.InjectEntityController(player);
            return player;
        }
    }
}