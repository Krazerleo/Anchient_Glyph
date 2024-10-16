using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.TraitModifiers;

namespace AncientGlyph.GameScripts.EntityModel.Traits
{
    public class TraitModsApplicator
    {
        private readonly List<HealthModifier> _healthModifiers = new(4);
        private readonly List<SanityModifier> _sanityModifiers = new(4);

        public void AppendHealthModifiers(HealthModifier mod) => AppendModifier(_healthModifiers, mod);
        public void RemoveHealthModifiers(HealthModifier mod) => RemoveModifier(_healthModifiers, mod);

        public int CalculateHealthTrait(int originalHealthPoints) =>
            _healthModifiers.Sum(h => h.HealthPointToAdd) + originalHealthPoints;

        public void AppendSanityModifiers(SanityModifier mod) => AppendModifier(_sanityModifiers, mod);
        public void RemoveSanityModifiers(SanityModifier mod) => RemoveModifier(_sanityModifiers, mod);

        public int CalculateSanityTrait(int originalSanityPoints) =>
            _sanityModifiers.Sum(m => m.SanityPointsToAdd) + originalSanityPoints;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendModifier<TMod>(List<TMod> modList, TMod modToAppend) where TMod : ITraitModifier =>
            modList.Add(modToAppend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RemoveModifier<TMod>(List<TMod> modList, TMod modToRemove) where TMod : ITraitModifier =>
            modList.RemoveAll(m => m.TraitModId == modToRemove.TraitModId);
    }
}