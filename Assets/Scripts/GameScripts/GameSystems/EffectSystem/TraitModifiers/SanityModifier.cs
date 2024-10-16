using System;
using AncientGlyph.GameScripts.EntityModel.Traits;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.TraitModifiers
{
    public class SanityModifier : ITraitModifier
    {
        public readonly int SanityPointsToAdd;
        public Guid TraitModId { get; } = Guid.NewGuid();

        public SanityModifier(int sanityPointsToAdd) 
            => SanityPointsToAdd = sanityPointsToAdd;

        public void ApplyTo(TraitModsApplicator applicator)
        {
            applicator.AppendSanityModifiers(this);
        }

        public void RemoveFrom(TraitModsApplicator applicator)
        {
            applicator.RemoveSanityModifiers(this);
        }
    }
}