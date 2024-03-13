using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions
{
    public interface IAction
    {
        string Id { get; }
        
        int CalculatePower();
        bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel);
        void Execute(CreatureController creatureController, PlayerModel playerModel);
        IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel);
    }
}