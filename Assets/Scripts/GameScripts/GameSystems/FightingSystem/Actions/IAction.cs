using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions
{
    public interface IAction
    {
        string Id { get; }
        
        int CalculatePower();
        bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel, IMoveBehaviour moveBehaviour);
        void Execute(CreatureController creatureController, PlayerController playerController);
        IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel, IMoveBehaviour moveBehaviour);
    }
}