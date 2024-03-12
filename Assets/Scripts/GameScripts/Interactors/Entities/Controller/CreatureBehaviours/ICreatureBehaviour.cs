using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours
{
    public interface ICreatureBehaviour
    {
        IAction PlanForTurn(CreatureModel creatureModel,
                         PlayerModel playerModel,
                         LevelModel levelModel);
    }
}