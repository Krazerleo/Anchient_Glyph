using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions
{
    public interface IFeedbackAction
    {
        string Identifier { get; }
        
        int CalculatePriority();
        
        void Execute(CreatureController controller);
    }
}