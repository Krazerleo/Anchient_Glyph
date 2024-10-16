using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.EntityModel.Controller;
using AncientGlyph.GameScripts.GameSystems.EffectSystem;

namespace AncientGlyph.GameScripts.GameSystems.QuirkSystem
{
    public interface IReactiveQuirk
    {
        /// <summary>
        /// Add this reactive quirk to one of the reaction hooks of dispatcher 
        /// </summary>
        /// <param name="dispatcher">Reaction dispatcher to apply</param>
        void ApplyTo(ReactionDispatcher dispatcher);

        bool Precondition();

        /// <summary>
        /// Execute reaction on some event which applied on subject
        /// </summary>
        public (IEffect effect, ReactiveQuirkTarget target) ExecuteReaction(IEffect appliedEffect,
                                                                            PlayerModel playerModel);
    }
}