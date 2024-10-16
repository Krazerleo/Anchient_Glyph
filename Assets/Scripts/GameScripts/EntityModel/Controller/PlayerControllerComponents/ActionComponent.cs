using System;
using AncientGlyph.GameScripts.InputSystem.PlayerActionInput;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.EntityModel.Controller.PlayerControllerComponents
{
    public class ActionComponent
    {
        private readonly IPlayerActionInput _actionInput;
        private InputRegistry _registry;

        public ActionComponent(IPlayerActionInput actionInput)
        {
            _actionInput = actionInput;
        }

        public void RegisterActionEvents(InputRegistry registry)
        {
            _registry = registry;
            _registry.EnableInputHandler += EnableActionControls;
            _registry.DisableInputHandler += DisableActionControls;
        }

        public void DeregisterActionEvents()
        {
            _registry.EnableInputHandler -= EnableActionControls;
            _registry.DisableInputHandler -= DisableActionControls;
        }

        private void EnableActionControls(object obj, EventArgs args)
        {
            _actionInput.FirstHandAction.performed += DoFirstHandAction;
            _actionInput.SecondHandAction.performed += DoSecondHandAction;
        }

        private void DisableActionControls(object obj, EventArgs args)
        {
            _actionInput.FirstHandAction.performed -= DoFirstHandAction;
            _actionInput.SecondHandAction.performed -= DoSecondHandAction;
        }

        private void DoFirstHandAction(InputAction.CallbackContext context) { }

        private void DoSecondHandAction(InputAction.CallbackContext context) { }
    }
}