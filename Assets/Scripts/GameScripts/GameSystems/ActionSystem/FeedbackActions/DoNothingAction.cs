using AncientGlyph.GameScripts.EntityModel.Controller;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions
{
    public class DoNothingAction : IFeedbackAction
    {
        public string Identifier => nameof(DoNothingAction);
        
        public int CalculatePriority() => -1;
        
        public void Execute(CreatureController controller)
        {
        }
    }
}