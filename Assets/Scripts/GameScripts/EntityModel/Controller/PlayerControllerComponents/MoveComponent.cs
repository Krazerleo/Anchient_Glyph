using System;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.InputSystem.PlayerMoveInput;
using AncientGlyph.GameScripts.Services.LoggingService;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.EntityModel.Controller.PlayerControllerComponents
{
    public class MoveComponent
    {
        private readonly IPlayerMoveInput _moveInput;
        private readonly PlayerAnimator _playerAnimator;
        private readonly LevelModel _levelModel;
        private readonly PlayerModel _playerEntity;
        private readonly ILoggingService _loggingService;
        
        private InputRegistry _registry;
        
        public MoveComponent(IPlayerMoveInput moveInput, PlayerAnimator animator, LevelModel levelModel,
                             PlayerModel playerEntity, ILoggingService loggingService)
        {
            _moveInput = moveInput;
            _playerAnimator = animator;
            _levelModel = levelModel;
            _loggingService = loggingService;
            _playerEntity = playerEntity;
        }
        
        public void RegisterMoveEvents(InputRegistry registry)
        {
            _registry = registry;
            _registry.EnableInputHandler += EnableMoveControls;
            _registry.DisableInputHandler += DisableMoveControls;
        }
        
        public void DeregisterMoveEvents()
        {
            _registry.EnableInputHandler -= EnableMoveControls;
            _registry.DisableInputHandler -= DisableMoveControls;
        }

        private void EnableMoveControls(object obj, EventArgs args)
        {
            _moveInput.MoveForwardAction.performed += OnMoveForward;
            _moveInput.MoveBackwardAction.performed += OnMoveBackward;
            _moveInput.MoveLeftAction.performed += OnMoveLeft;
            _moveInput.MoveRightAction.performed += OnMoveRight;
        }
        
        private void DisableMoveControls(object obj, EventArgs handler)
        {
            _moveInput.MoveForwardAction.performed -= OnMoveForward;
            _moveInput.MoveBackwardAction.performed -= OnMoveBackward;
            _moveInput.MoveLeftAction.performed -= OnMoveLeft;
            _moveInput.MoveRightAction.performed -= OnMoveRight;
        }

        private void OnMoveForward(InputAction.CallbackContext context)
            => Move(0, 0, 1);

        private void OnMoveBackward(InputAction.CallbackContext context)
            => Move(0, 0, -1);

        private void OnMoveRight(InputAction.CallbackContext context)
            => Move(1, 0, 0);

        private void OnMoveLeft(InputAction.CallbackContext context)
            => Move(-1, 0, 0);
        
        private void Move(int xOffset, int yOffset, int zOffset)
        {
            Vector3Int offsetVector = new(xOffset, yOffset, zOffset);
            
            if (_playerEntity.TryMoveToNextCell(offsetVector, _levelModel))
            {
                _playerAnimator.Move(offsetVector);
                bool completionResult = _registry.RegisterInput();
                
                if (completionResult == false)
                {
                    _loggingService.LogError("Cannot register player input");
                }
            }
        }
    }
}