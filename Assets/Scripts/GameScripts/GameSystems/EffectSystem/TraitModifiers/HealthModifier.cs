using System;
using AncientGlyph.GameScripts.EntityModel.Traits;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem.TraitModifiers
{
    public class HealthModifier : ITraitModifier
    {
        public readonly int HealthPointToAdd;
        public Guid TraitModId { get; } = Guid.NewGuid();

        public HealthModifier(int healthPointToAdd) => HealthPointToAdd = healthPointToAdd;

        public void ApplyTo(TraitModsApplicator applicator) => applicator.AppendHealthModifiers(this);

        public void RemoveFrom(TraitModsApplicator applicator) => applicator.RemoveHealthModifiers(this);
    }
}