using System;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions
{
    [Serializable]
    public class OnLineCondition : IActionCondition
    {
        [SerializeField]
        private int _minDistanceFromTarget;

        [SerializeField]
        private int _maxDistanceFromTarget;

        public OnLineCondition() {}
        
        public OnLineCondition(int minDistanceFromTarget, int maxDistanceFromTarget)
        {
            _minDistanceFromTarget = minDistanceFromTarget;
            _maxDistanceFromTarget = maxDistanceFromTarget;
        }

        public bool CanExecute(IEntityModel self, IEntityModel target,
            IMoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            if (self.Position.y != target.Position.y)
            {
                return false;
            }

            if (self.Position.x != target.Position.x &&
                self.Position.z != target.Position.z)
            {
                return false;
            }

            var distance = Math.Max(
                Math.Abs(self.Position.x - target.Position.x),
                Math.Abs(self.Position.z - target.Position.z));

            if (distance < _minDistanceFromTarget || distance > _maxDistanceFromTarget)
            {
                return false;
            }

            return levelModel.IsRayCollided(self.Position, target.Position) == false;
        }

        // TODO : Implement more smart and distance sensitive solution
        // Step 1 : Calculate positions on two nearest lines to target considering min_max distance
        // Step 2 : For each position calculate distance and take one with minimum distance.
        public IFeedbackAction GetFeedback(IEntityModel self, IEntityModel target,
            IMoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            var offset = moveBehaviour.CalculateNextStep(self.Position, target.Position);

            if (offset == null)
            {
                return new DoNothingAction();
            }

            return new GoToAction(offset.Value);
        }
    }
}