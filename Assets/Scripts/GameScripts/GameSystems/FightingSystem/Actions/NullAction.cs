using System;
using AncientGlyph.GameScripts.Interactors.Entities;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions
{
    public class NullAction : IAction
    {
        public string Id => nameof(NullAction);

        public int CalculatePower() => -1;

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel) => false;

        public void Execute()
        {
        }

        public IAction GetFeedback() => throw new InvalidOperationException(nameof(NullAction));
    }
}