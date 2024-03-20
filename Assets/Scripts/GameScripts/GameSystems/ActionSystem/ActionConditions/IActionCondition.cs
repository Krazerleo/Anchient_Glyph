using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.ActionConditions
{
    public interface IActionCondition
    {
        public bool CanExecute(IEntityModel self, IEntityModel target,
            IMoveBehaviour moveBehaviour, LevelModel levelModel);

        public IFeedbackAction GetFeedback(IEntityModel self, IEntityModel target,
            IMoveBehaviour moveBehaviour, LevelModel levelModel);
    }
}