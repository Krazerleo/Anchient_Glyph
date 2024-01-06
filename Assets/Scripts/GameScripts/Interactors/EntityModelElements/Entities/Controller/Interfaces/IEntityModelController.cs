using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.Interactors.Creatures.Controllers.Interfaces
{
    public interface IEntityModelController
    {
        public int TurnsCount { get; }

        public IEntityModel CreatureEntity { get; }

        public void MakeNextTurn();
    }
}