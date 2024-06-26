using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours;
using AncientGlyph.GameScripts.GameSystems.ActionSystem;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Controller
{
    public class CreatureController : IEntityController, IEffectAcceptor
    {
        public bool IsEnabled => true;

        public IEntityModel EntityModel => _creatureModel;

        private readonly CreatureModel _creatureModel;
        private readonly PlayerController _playerController;
        private readonly LevelModel _levelModel;
        private readonly CreatureAnimator _animator;
        private readonly ICreatureBehaviour _behaviour;
        private readonly ILoggingService _loggingService;

        public CreatureController(CreatureModel entityModel,
            PlayerController playerController,
            LevelModel levelModel,
            CreatureAnimator animator,
            ICreatureBehaviour behaviour,
            ILoggingService loggingService)
        {
            _creatureModel = entityModel;
            _playerController = playerController;
            _levelModel = levelModel;
            _animator = animator;
            _behaviour = behaviour;
            _loggingService = loggingService;
        }

        public UniTask MakeNextTurn()
        {
            (ApplyEffectsAction PlannedAction, IFeedbackAction FeedbackAction) decision = 
                _behaviour.PlanForTurn(_creatureModel, _playerController.EntityModel, _levelModel);

            if (decision.PlannedAction != null)
            {
                decision.PlannedAction.Execute(this, _playerController);
                return UniTask.CompletedTask;
            }

            if (decision.FeedbackAction != null)
            {
                decision.FeedbackAction.Execute(this);
                return UniTask.CompletedTask;
            }
                
            return UniTask.CompletedTask;
        }

        public void ExecuteMove(GoToAction goToAction)
        {
            Vector3Int offset = goToAction.Offset;

            if (_creatureModel.TryMoveToNextCell(offset, _levelModel))
            {
                _animator.Move(offset);
                return;
            }

            _loggingService.LogWarning("Creature made a stupid move: " +
                                       $"{_creatureModel.Position} -> " +
                                       $"{offset + _creatureModel.Position}");
        }

        public void AcceptDamageEffect(DamageEffect damageEffect)
        {
            throw new System.NotImplementedException("damage effect");
        }

        public void AcceptGoToEffect(GoToEffect goToEffect)
        {
            throw new System.NotImplementedException();
        }
    }
}