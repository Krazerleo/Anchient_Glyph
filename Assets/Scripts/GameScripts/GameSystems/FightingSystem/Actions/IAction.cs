using AncientGlyph.GameScripts.Interactors.Entities;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions
{
    public interface IAction
    {
        string Id { get; }
        
        int CalculatePower();
        bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel);
        void Execute();
        IAction GetFeedback();
    }
}