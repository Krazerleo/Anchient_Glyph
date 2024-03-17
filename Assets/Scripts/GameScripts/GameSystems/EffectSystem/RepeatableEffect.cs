using AncientGlyph.GameScripts.EntityModel;

namespace AncientGlyph.GameScripts.GameSystems.EffectSystem
{
    public class RepeatableEffect
    {
        private readonly IEffect _effect;
        private readonly IEffectAcceptor _owner;
        public int TurnsLeft { get; private set; }
        
        public RepeatableEffect(IEffect effect, IEffectAcceptor owner, int ticks)
        {
            _effect = effect;
            _owner = owner;
            TurnsLeft = ticks;
        }

        public bool Tick()
        {
            if (TurnsLeft != 0)
            {
                _effect.ApplyOn(_owner);
            }

            TurnsLeft--;
            return TurnsLeft != 0;
        }
    }
}