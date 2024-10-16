using System;
using AncientGlyph.GameScripts.EntityModel.Traits;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.TraitModifiers
{
    public interface ITraitModifier
    {
        Guid TraitModId { get; }
        
        void ApplyTo(TraitModsApplicator applicator);

        void RemoveFrom(TraitModsApplicator applicator);
    }
}