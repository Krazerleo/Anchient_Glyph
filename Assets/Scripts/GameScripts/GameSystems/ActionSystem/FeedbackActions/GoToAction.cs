using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions
{
    public class GoToAction : IFeedbackAction
    {
        public readonly Vector3Int Offset;
        
        public GoToAction(Vector3Int offset)
        {
            Offset = offset;
        }
        
        public string Identifier => nameof(GoToAction);
        
        public int CalculatePriority() => 1;
        
        public void Execute(CreatureController controller)
        {
            controller.ExecuteMove(this);
        }
    }
}