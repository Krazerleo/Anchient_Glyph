using AncientGlyph.GameScripts.Animators;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Interaction;
using AncientGlyph.GameScripts.PlayerInput;
using AncientGlyph.GameScripts.Services.LoggingService;

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controllers
{
    public class PlayerController : IEntityController
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
        public IInteraction PlayerAction;
        public IEntityModel InteractedEntity;
        public bool IsEnabled => true;

        private readonly ILoggingService _loggingService;
        private readonly PlayerMoveInput _playerMoveInput;
        private readonly LevelModel _levelModel;
        private readonly PlayerModel _playerEntity;
        private readonly PlayerAnimator _playerAnimator;
        private AutoResetUniTaskCompletionSource _actionCompletionSource;

        public UniTask MakeNextTurn()
        {
            AddActionEvents();

            return _actionCompletionSource.Task;
        }

        private void AddActionEvents()
        {
            _playerMoveInput.MoveBackwardAction.action.performed += OnMoveForward;
            _playerMoveInput.MoveBackwardAction.action.performed += OnMoveBackward;
            _playerMoveInput.MoveLeftAction.action.performed += OnMoveLeft;
            _playerMoveInput.MoveRightAction.action.performed += OnMoveRight;
        }

        private void RemoveActionEvents()
        {
            _playerMoveInput.MoveBackwardAction.action.performed -= OnMoveForward;
            _playerMoveInput.MoveBackwardAction.action.performed -= OnMoveBackward;
            _playerMoveInput.MoveLeftAction.action.performed -= OnMoveLeft;
            _playerMoveInput.MoveRightAction.action.performed -= OnMoveRight;
        }

        private void OnMoveForward(InputAction.CallbackContext context)
            => Move(0, 0, 1);

        private void OnMoveBackward(InputAction.CallbackContext context)
            => Move(0, 0, -1);

        private void OnMoveLeft(InputAction.CallbackContext context)
            => Move(1, 0, 0);

        private void OnMoveRight(InputAction.CallbackContext context)
            => Move(-1, 0, 0);

        private void Move(int xOffset, int yOffset, int zOffset)
        {
            Debug.Log("walked");

            if (_levelModel.TryMoveEntity(_playerEntity, xOffset, yOffset, zOffset))
            {
                RemoveActionEvents();
                _playerAnimator.Move(new Vector3Int(xOffset, yOffset, zOffset));

                if (_actionCompletionSource.TrySetResult() == false)
                {
                    _loggingService.LogError("Cannot set task result on completion");
                }
            }
        }
    }
}