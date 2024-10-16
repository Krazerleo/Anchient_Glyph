using Newtonsoft.Json.Linq;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.InputSystem.PlayerActionInput
{
    public class PlayerActionInput : IPlayerActionInput
    {
        public InputAction FirstHandAction { get; set; }
        public InputAction SecondHandAction { get; set; }

        void ReadActionInputConfig(JObject actionConfig)
        {
            const string FirstHandKey = "first_hand";
            const string SecondHandKey = "second_hand";
        }
    }
}