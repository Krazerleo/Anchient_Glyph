using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours
{
    public interface IActionExecutor
    {
        public void ExecuteMove(GoToAction goToAction);
    }
}