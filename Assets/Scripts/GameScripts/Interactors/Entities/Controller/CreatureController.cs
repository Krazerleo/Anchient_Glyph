using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities.Controller._Interfaces;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller
{
    public class CreatureController : IEntityController
    {
        public bool IsEnabled { get; } = true;

        public IEntityModel EntityModel => _creatureModel;

        private readonly CreatureModel _creatureModel;
        private readonly LevelModel _levelModel;
        private readonly CreatureAnimator _animator;
        private readonly ICreatureBehaviour _behaviour;

        public CreatureController(CreatureModel entityModel,
                                  LevelModel levelModel,
                                  CreatureAnimator animator,
                                  ICreatureBehaviour behaviour)
        {
            _creatureModel = entityModel;
            _levelModel = levelModel;
            _animator = animator;
            _behaviour = behaviour;
        }

        public UniTask MakeNextTurn()
        {
            _behaviour.PlanForTurn(_creatureModel, _levelModel);
            var randomOffset = new Vector3Int(0, 0, 1);
            
            if (_levelModel.TryMoveEntity(_creatureModel, randomOffset))
            {
                _creatureModel.Position += randomOffset;
                _animator.Move(randomOffset);
            }
            
            return UniTask.CompletedTask;
        }
    }
}