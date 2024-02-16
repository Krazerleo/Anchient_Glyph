using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Services.LoggingService;

using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controllers
{
    public class CreatureController : IEntityController
    {
        public bool IsEnabled => _isEnabled;
        public IEntityModel EntityModel => _creatureModel;

        private bool _isEnabled = true;
        private CreatureModel _creatureModel;

        private LevelModel _levelModel;
        private ILoggingService _loggingService;
        private CreatureAnimator _animator;
        private ICreatureBehaviour _behaviour;

        public CreatureController(CreatureModel entityModel,
                                  LevelModel levelModel,
                                  ILoggingService loggingService,
                                  CreatureAnimator animator,
                                  ICreatureBehaviour behaviour)
        {
            _creatureModel = entityModel;
            _levelModel = levelModel;
            _loggingService = loggingService;
            _animator = animator;
            _behaviour = behaviour;
        }

        public UniTask MakeNextTurn()
        {
            _behaviour.PlanForTurn(_creatureModel, _levelModel);
            return UniTask.CompletedTask;
        }
    }
}