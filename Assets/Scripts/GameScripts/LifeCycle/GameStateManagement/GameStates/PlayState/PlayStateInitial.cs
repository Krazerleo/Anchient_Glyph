using AncientGlyph.GameScripts.CoreGameMechanics;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Factory._Interfaces;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
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
            InjectCreaturesToGameLoop()
                .ContinueWith(InjectPlayerToGameLoop)
                .ContinueWith(_gameLoop.StartGameLoop)
                .Forget();
        }

        public void Exit()
        {
        }

        public void LateStateMachineBinding(IGameStateMachine stateMachine)
            => _stateMachine = stateMachine;

        private async UniTask InjectCreaturesToGameLoop()
        {
            var entities = _levelModel.GetAllCurrentEntities();
            
            foreach (var entity in entities)
            {
                var controller = await _creatureFactory
                    .CreateCreature(entity.Position, entity as CreatureModel);

                _gameLoop.InjectEntityController(controller);
            }
        }

        private async UniTask InjectPlayerToGameLoop()
        {
            var player = await _playerFactory.CreatePlayer();
            
            _gameLoop.InjectEntityController(player);
        }
    }
}