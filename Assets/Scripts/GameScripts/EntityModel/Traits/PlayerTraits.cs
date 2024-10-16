using System.Collections.Generic;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.TraitModifiers;
using AncientGlyph.GameScripts.GameSystems.QuirkSystem;

namespace AncientGlyph.GameScripts.EntityModel.Traits
{
    public class PlayerTraits
    {
        public int Vitality { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        
        public int HandCrafting { get; set; }
        public int LockPicking { get; set; }
        public int Engineering { get; set; }
        public int Alchemy { get; set; }
        public int Exploration { get; set; }
        
        public int MeleeCombat { get; set; }
        public int DistantCombat { get; set; }
        public int ConcentrationControl { get; set; }
        public int ArmorAndBlocking { get; set; }
        public int BattleTactics { get; set; }
        
        private readonly TraitModsApplicator _modsApplicator = new();
        private readonly List<IReactiveQuirk> _reactiveQuirks = new();

        public int CalculateMaxHealthPoints()
        {
            int healthPoints = 2 + Vitality;
            return _modsApplicator.CalculateHealthTrait(healthPoints);
        }

        public int CalculateMaxSanityPoints()
        {
            int sanityPoints = 1 + Intelligence;
            return _modsApplicator.CalculateSanityTrait(sanityPoints);
        }

        public void ApplyTraitMod(ITraitModifier modifier)
        {
            modifier.ApplyTo(_modsApplicator);
        }

        public void RemoveTraitMod(ITraitModifier modifier)
        {
            modifier.RemoveFrom(_modsApplicator);
        }
    }
}