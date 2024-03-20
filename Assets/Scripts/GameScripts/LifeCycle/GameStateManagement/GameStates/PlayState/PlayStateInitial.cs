using AncientGlyph.GameScripts.CoreGameMechanics;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Factory._Interfaces;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates.PlayState
{
    [UsedImplicitly]
    public class PlayStateInitial : IGameState
    {
        private IGameStateMachine _stateMachine;
        private readonly ICreatureFactory _creatureFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly LevelModel _levelModel;
        private readonly GameLoop _gameLoop;

        public PlayStateInitial(ICreatureFactory creatureFactory,
                                IPlayerFactory playerFactory,
                                LevelModel levelModel,
                                GameLoop gameLoop)
        {
            _creatureFactory = creatureFactory;
            _playerFactory = playerFactory;
            _levelModel = levelModel;
            _gameLoop = gameLoop;
        }

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            InjectPlayerToGameLoop()
                .ContinueWith(InjectCreaturesToGameLoop)
                .ContinueWith(_gameLoop.StartGameLoop)
                .Forget();
        }

        public void Exit()
        {
        }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
            => _stateMachine = stateMachine;

        private async UniTask InjectCreaturesToGameLoop(PlayerController playerController)
        {
            var entities = _levelModel.GetAllCurrentEntities();
            
            foreach (var entity in entities)
            {
                var controller = await _creatureFactory
                    .CreateCreature(entity.Position, entity as CreatureModel, playerController);

                _gameLoop.InjectEntityController(controller);
            }
        }

        private async UniTask<PlayerController> InjectPlayerToGameLoop()
        {
            var player = await _playerFactory.CreatePlayer();
            
            _gameLoop.InjectEntityController(player);
            return player;
        }
    }
}