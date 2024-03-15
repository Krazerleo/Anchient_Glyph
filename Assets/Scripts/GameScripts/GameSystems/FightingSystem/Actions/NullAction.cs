using System;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions
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