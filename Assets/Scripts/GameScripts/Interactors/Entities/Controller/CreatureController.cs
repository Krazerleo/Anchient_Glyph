using System.Collections.Generic;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.CombatActions.MeleeCombat;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.FeedbackActions;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours;
using AncientGlyph.GameScripts.Interactors.Interactions;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller
{
    public class CreatureController : IEntityController, IActionExecutor
    {
        public bool IsEnabled => true;

        public IEntityModel EntityModel => _creatureModel;

        private readonly CreatureModel _creatureModel;
        private readonly LevelModel _levelModel;
        private readonly CreatureAnimator _animator;
        private readonly ICreatureBehaviour _behaviour;
        private readonly PlayerModel _playerModel;
        private readonly ILoggingService _loggingService;

        public CreatureController(CreatureModel entityModel,
            PlayerModel playerModel,
            LevelModel levelModel,
            CreatureAnimator animator,
            ICreatureBehaviour behaviour, 
            ILoggingService loggingService)
        {
            _creatureModel = entityModel;
            _playerModel = playerModel;
            _levelModel = levelModel;
            _animator = animator;
            _behaviour = behaviour;
            _loggingService = loggingService;
        }

        public UniTask MakeNextTurn()
        {
            var decision = _behaviour.PlanForTurn(_creatureModel, _playerModel, _levelModel);
            
            decision.Execute(this, _playerModel);
            
            var randomOffset = new Vector3Int(0, 0, 1);

            if (_levelModel.TryMoveEntity(_creatureModel, randomOffset))
            {
                _creatureModel.Position += randomOffset;
                _animator.Move(randomOffset);
            }

            return UniTask.CompletedTask;
        }

        public void AcceptInteraction(HitInteraction hit)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptInteraction(FunctionalInteraction func)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptInteraction(ICollection<GameItem> listItems)
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteMove(GoToAction goToAction)
        {
            var offset = goToAction.Offset;
            
            if (!_levelModel.TryMoveEntity(_creatureModel, offset) && offset.sqrMagnitude != 0)
            {
                _loggingService.LogWarning("Creature made stupid move: " +
                                           $"{_creatureModel.Position} -> " +
                                           $"{offset + _creatureModel.Position}");
                
                return;
            }
            
            _creatureModel.Position += offset;
            _animator.Move(offset);
        }

        public void ExecuteMeleeCombatAction(MeleeCombatAction combatAction)
        {
            throw new System.NotImplementedException();
        }
    }
}