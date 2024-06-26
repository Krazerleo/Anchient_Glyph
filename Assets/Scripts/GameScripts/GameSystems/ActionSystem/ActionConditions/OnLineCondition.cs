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

        public OnLineCondition() { }

        public OnLineCondition(int minDistanceFromTarget, int maxDistanceFromTarget)
        {
            _minDistanceFromTarget = minDistanceFromTarget;
            _maxDistanceFromTarget = maxDistanceFromTarget;
        }

        public bool CanExecute(IEntityModel self, IEntityModel target,
            MoveBehaviour moveBehaviour, LevelModel levelModel)
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

            int distance = Math.Max(
                Math.Abs(self.Position.x - target.Position.x),
                Math.Abs(self.Position.z - target.Position.z));

            if (distance < _minDistanceFromTarget || distance > _maxDistanceFromTarget)
            {
                return false;
            }

            return levelModel.IsRayCollided(self.Position, target.Position) == false;
        }
        
        public IFeedbackAction GetFeedback(IEntityModel self, IEntityModel target,
            MoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            Vector3Int? offset = moveBehaviour.CalculateNextStepToLine(self.Position, target.Position);

            if (offset == null)
            {
                return new DoNothingAction();
            }

            return new GoToAction(offset.Value);
        }
    }
}