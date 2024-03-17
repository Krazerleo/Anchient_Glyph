using System;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem
{
    public class NullAction : IAction
    {
        public string Id => nameof(NullAction);
        public int CalculatePower() => -1;

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour) => false;

        public void Execute(CreatureController creatureController, PlayerController playerController)
        {
        }

        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour) => throw new InvalidOperationException(nameof(NullAction));
    }
}