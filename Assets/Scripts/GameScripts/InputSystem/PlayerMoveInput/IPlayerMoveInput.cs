using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.InputSystem.PlayerMoveInput
{
    public interface IPlayerMoveInput
    {
        public InputAction MoveForwardAction { get; }
        public InputAction MoveBackwardAction { get; }
        public InputAction MoveRightAction { get; }
        public InputAction MoveLeftAction { get; }
    }
}