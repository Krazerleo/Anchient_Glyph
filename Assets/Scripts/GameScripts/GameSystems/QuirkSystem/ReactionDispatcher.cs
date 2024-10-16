using System.Collections.Generic;

namespace AncientGlyph.GameScripts.GameSystems.QuirkSystem
{
    public class ReactionDispatcher
    {
        private readonly List<IReactiveQuirk> AfterGettingDamageHooks = new(1);
        private readonly List<IReactiveQuirk> BeforeGettingDamageHooks = new(1);

        public void AddAfterGettingDamageHook(IReactiveQuirk quirk) => AfterGettingDamageHooks.Add(quirk);
        public void AddBeforeGettingDamageHook(IReactiveQuirk quirk) => BeforeGettingDamageHooks.Add(quirk);
    }
}