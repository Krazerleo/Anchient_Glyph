using System;
using AncientGlyph.GameScripts.AlgorithmsAndStructures;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions
{
    public class OnLineCondition : IActionCondition
    {
        private readonly int _minDistanceFromTarget;
        private readonly int _maxDistanceFromTarget;

        public OnLineCondition(int minDistanceFromTarget, int maxDistanceFromTarget)
        {
            _minDistanceFromTarget = minDistanceFromTarget;
            _maxDistanceFromTarget = maxDistanceFromTarget;
        }

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            if (creatureModel.Position.y != playerModel.Position.y)
            {
                return false;
            }

            if (creatureModel.Position.x != playerModel.Position.x &&
                creatureModel.Position.z != playerModel.Position.z)
            {
                return false;
            }

            var distance = Math.Max(
                Math.Abs(creatureModel.Position.x - playerModel.Position.x),
                Math.Abs(creatureModel.Position.z - playerModel.Position.z));

            if (distance < _minDistanceFromTarget || distance > _maxDistanceFromTarget)
            {
                return false;
            }

            return CellRayCaster.IsRayCollided(creatureModel.Position, playerModel.Position, levelModel);
        }

        // TODO : Implement more smart and distance sensitive solution
        // Step 1 : Calculate positions on two nearest lines to target considering min_max distance
        // Step 2 : 
        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            var offset = moveBehaviour.CalculateNextStep(creatureModel.Position, playerModel.Position);

            if (offset == null)
            {
                return new NullAction();
            }

            return new GoToAction(offset.Value);
        }
    }
}