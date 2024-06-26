using System;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions
{
    [Serializable]
    public class OnCloseCondition : IActionCondition
    {
        public OnCloseCondition() { }
        
        public bool CanExecute(IEntityModel self, IEntityModel target,
            MoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            if (self.Position.y != target.Position.y)
            {
                return false;
            }
            
            if (Math.Abs(self.Position.x - target.Position.x) + 
                Math.Abs(self.Position.z - target.Position.z) != 1)
            {
                return false;
            }

            return levelModel.IsRayCollided(self.Position, target.Position);
        }

        public IFeedbackAction GetFeedback(IEntityModel self, IEntityModel target,
            MoveBehaviour moveBehaviour, LevelModel levelModel)
        {
            Vector3Int? offset = moveBehaviour.CalculateNextStepToPoint(self.Position, target.Position);

            if (offset == null)
            {
                return new DoNothingAction();
            }

            return new GoToAction(offset.Value);
        }
    }
}