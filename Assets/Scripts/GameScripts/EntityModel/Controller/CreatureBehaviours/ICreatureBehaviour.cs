using AncientGlyph.GameScripts.GameSystems.ActionSystem;
using AncientGlyph.GameScripts.GameSystems.ActionSystem.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours
{
    public interface ICreatureBehaviour
    {
        (ApplyEffectsAction PlannedAction, IFeedbackAction FeedbackAction) 
            PlanForTurn(CreatureModel creatureModel, PlayerModel playerModel, LevelModel levelModel);
    }
}