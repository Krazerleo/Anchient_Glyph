using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.CombatActions.MeleeCombat;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.FeedbackActions;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours
{
    public interface IActionExecutor
    {
        public void ExecuteMove(GoToAction goToAction);
        public void ExecuteMeleeCombatAction(MeleeCombatAction combatAction);
    }
}