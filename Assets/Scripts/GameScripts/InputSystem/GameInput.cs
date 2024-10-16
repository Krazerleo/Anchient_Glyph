using AncientGlyph.GameScripts.InputSystem.PlayerActionInput;
using AncientGlyph.GameScripts.InputSystem.PlayerMoveInput;
using Newtonsoft.Json.Linq;

namespace AncientGlyph.GameScripts.InputSystem
{
    public class GameInput
    {
        public void ReadInputConfig(JObject config)
        {
            
        }

        public IPlayerMoveInput MoveInput { get; }
        public IPlayerActionInput ActionInput { get; }
    }
}