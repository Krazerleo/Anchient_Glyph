using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.InputSystem.PlayerActionInput
{
    public interface IPlayerActionInput
    {
        public InputAction FirstHandAction { get; }
        public InputAction SecondHandAction { get; }
    }
}