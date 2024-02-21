using System.Linq;
using AncientGlyph.GameScripts.CoreGameMechanics;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controllers;
using AncientGlyph.GameScripts.Interactors.Entities.Factories;
using AncientGlyph.GameScripts.Interactors.EntityModelElements.Entities;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.GameStates;
using AncientGlyph.GameScripts.LifeCycle.GameStateManagment.StateMachine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.LifeCycle.GameStateManagement.GameStates.PlayState
{
    [UsedImplicitly]
    public class PlayStateInitial : IGameState
    {
        private IGameStateMachine _stateMachine;
        private readonly ICreatureFactory _creatureFactory;
        private readonly PlayerController _playerController;
        private readonly LevelModel _levelModel;
        private readonly GameLoop _gameLoop;

        public PlayStateInitial(ICreatureFactory creatureFactory,
                                PlayerController playerController,
                                LevelModel levelModel,
                                GameLoop gameLoop)
        {
            _creatureFactory = creatureFactory;
            _playerController = playerController;
            _levelModel = levelModel;
            _gameLoop = gameLoop;
        }

        public void Enter<TNextStateParams>(TNextStateParams parameters)
        {
            InjectCreaturesToGameLoop()
                .ContinueWith(() =>
                {
                    _gameLoop.InjectEntityController(_playerController);
                    _gameLoop.StartGameLoop().Forget();
                })
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
    }
}