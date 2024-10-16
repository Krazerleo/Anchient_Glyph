using Newtonsoft.Json.Linq;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.InputSystem.PlayerMoveInput
{
    public class PlayerMoveInput : IPlayerMoveInput
    {
        private const string MoveForwardActionMappingName = "move_forward";
        public InputAction MoveForwardAction { get; set; }

        private const string MoveBackwardActionMappingName = "move_backward";
        public InputAction MoveBackwardAction { get; set; }
        
        private const string MoveRightActionMappingName = "move_right";
        public InputAction MoveRightAction { get; set; }
        
        private const string MoveLeftActionMappingName = "move_left";
        public InputAction MoveLeftAction { get; set; }

        public void ReadMoveInputConfig(JObject moveConfig)
        {
            InputConfigValidator validator = new();
            
            const string MoveForwardKey = "move_forward";
            const string MoveBackwardKey = "move_forward";
            const string MoveRightKey = "move_forward";
            const string MoveLeftKey = "move_left";

            string forwardKey = moveConfig[MoveForwardKey].ToString();
            if (validator.IsSimpleKeyboardKey(forwardKey))
            {
                MoveForwardAction = new InputAction(MoveForwardActionMappingName, binding: $"<Keyboard>/{forwardKey}");
            }
            
            
        }
    }
}