using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.CombatActions.MeleeCombat;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.EntityModel.Controller
{
    public class CreatureController : IEntityController, IEffectAcceptor, IActionExecutor
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
            var decision = _behaviour.PlanForTurn(_creatureModel,
                _playerController.EntityModel as PlayerModel,
                _levelModel);

            decision.Execute(this, _playerController);

            return UniTask.CompletedTask;
        }

        public void ExecuteMove(GoToAction goToAction)
        {
            var offset = goToAction.Offset;

            if (_creatureModel.TryMoveToNextCell(offset, _levelModel))
            {
                _animator.Move(offset);
                return;
            }

            _loggingService.LogWarning("Creature made a stupid move: " +
                                       $"{_creatureModel.Position} -> " +
                                       $"{offset + _creatureModel.Position}");
        }

        public void ExecuteMeleeCombatAction(MeleeCombatAction combatAction, IEffectAcceptor entity)
        {
            // TODO : 
        }

        public void AcceptDamageEffect(DamageEffect damageEffect)
        {
            throw new System.NotImplementedException();
        }
    }
}