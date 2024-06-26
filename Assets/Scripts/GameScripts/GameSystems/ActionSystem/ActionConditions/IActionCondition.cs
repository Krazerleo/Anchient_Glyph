using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions
{
    public interface IActionCondition
    {
        public bool CanExecute(IEntityModel self, IEntityModel target,
            MoveBehaviour moveBehaviour, LevelModel levelModel);

        public IFeedbackAction GetFeedback(IEntityModel self, IEntityModel target,
            MoveBehaviour moveBehaviour, LevelModel levelModel);
    }
}