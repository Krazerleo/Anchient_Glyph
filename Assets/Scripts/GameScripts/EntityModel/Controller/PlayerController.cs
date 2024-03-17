using System.Collections.Generic;
using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.GameSystems.EffectSystem.Effects;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.PlayerInput;
using AncientGlyph.GameScripts.Services.LoggingService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.EntityModel.Controller
{
    public class PlayerController : IEntityController, IEffectAcceptor
    {
        public PlayerController(PlayerAnimator playerAnimator,
                                PlayerMoveInput playerMoveInput,
                                LevelModel levelModel,
                                PlayerModel playerModel,
                                ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _playerAnimator = playerAnimator;
            _playerMoveInput = playerMoveInput;
            _levelModel = levelModel;
            _playerEntity = playerModel;

            _actionCompletionSource = AutoResetUniTaskCompletionSource.Create();
        }

        public IEntityModel EntityModel => _playerEntity;
        public bool IsEnabled => true;

        private readonly ILoggingService _loggingService;
        private readonly PlayerMoveInput _playerMoveInput;
        private readonly LevelModel _levelModel;
        private readonly PlayerModel _playerEntity;
        private readonly PlayerAnimator _playerAnimator;
        private readonly AutoResetUniTaskCompletionSource _actionCompletionSource;

        public UniTask MakeNextTurn()
        {
            AddActionEvents();

            return _actionCompletionSource.Task;
        }

        private void AddActionEvents()
        {
            _playerMoveInput.MoveForwardAction.action.performed += OnMoveForward;
            _playerMoveInput.MoveBackwardAction.action.performed += OnMoveBackward;
            _playerMoveInput.MoveLeftAction.action.performed += OnMoveLeft;
            _playerMoveInput.MoveRightAction.action.performed += OnMoveRight;
        }

        private void RemoveActionEvents()
        {
            _playerMoveInput.MoveForwardAction.action.performed -= OnMoveForward;
            _playerMoveInput.MoveBackwardAction.action.performed -= OnMoveBackward;
            _playerMoveInput.MoveLeftAction.action.performed -= OnMoveLeft;
            _playerMoveInput.MoveRightAction.action.performed -= OnMoveRight;
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
            var offsetVector = new Vector3Int(xOffset, yOffset, zOffset);
            
            if (_levelModel.TryMoveEntity(_playerEntity, offsetVector))
            {
                RemoveActionEvents();
                _playerEntity.Position += offsetVector;
                _playerAnimator.Move(offsetVector);

                if (_actionCompletionSource.TrySetResult() == false)
                {
                    _loggingService.LogError("Cannot set task result on completion");
                }
            }
        }

        public void AcceptDamageEffect(DamageEffect damageEffect)
        {
            throw new System.NotImplementedException();
        }
    }
}