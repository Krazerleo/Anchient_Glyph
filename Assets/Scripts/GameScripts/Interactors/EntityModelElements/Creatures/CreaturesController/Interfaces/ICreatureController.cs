using AncientGlyph.GameScripts.Interactors.Interfaces;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Creatures.Controllers.Interfaces
{
    public interface ICreatureModelController
    {
        public int TurnsCount { get; }

        public IEntityModel CreatureEntity { get; }

        public void MakeNextTurn();
    }
}