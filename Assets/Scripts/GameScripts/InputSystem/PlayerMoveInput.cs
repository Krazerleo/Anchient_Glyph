using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.PlayerInput
{
    public class PlayerMoveInput : MonoBehaviour
    {
        public InputActionReference MoveForwardAction;
        public InputActionReference MoveBackwardAction;
        public InputActionReference MoveRightAction;
        public InputActionReference MoveLeftAction;
    }
}