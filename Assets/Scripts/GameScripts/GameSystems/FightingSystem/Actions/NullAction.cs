using System;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions
{
    public class NullAction : IAction
    {
        public string Id => nameof(NullAction);
        public int CalculatePower() => -1;

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel) => false;

        public void Execute(CreatureController creatureController, PlayerModel playerModel)
        {
        }

        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel)
            => throw new InvalidOperationException(nameof(NullAction));
    }
}