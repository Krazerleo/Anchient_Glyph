using AncientGlyph.GameScripts.GameSystems.ActionSystem;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours
{
    public interface ICreatureBehaviour
    {
        IAction PlanForTurn(CreatureModel creatureModel,
                         PlayerModel playerModel,
                         LevelModel levelModel);
    }
}