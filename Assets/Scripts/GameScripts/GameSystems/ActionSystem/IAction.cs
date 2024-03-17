using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem
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