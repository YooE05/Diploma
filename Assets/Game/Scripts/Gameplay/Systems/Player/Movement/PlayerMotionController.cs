using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerMotionController : Listeners.IUpdateListener, Listeners.IStartListener
    {
        private readonly PlayerInput _playerInput;
        private readonly Camera _playerCamera;
        private readonly PlayerAnimation _playerAnimation;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerRotation _playerRotation;

        private bool _canAct;

        public PlayerMotionController(PlayerAnimation playerAnimation, PlayerMovement playerMovement,
            PlayerRotation playerRotation, Camera playerCamera, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerAnimation = playerAnimation;
            _playerMovement = playerMovement;
            _playerRotation = playerRotation;
            _playerCamera = playerCamera;

            DisableMotion();
        }

        public void OnStart()
        {
            _canAct = true;
            _playerMovement.EnableCharacterControllerComponent();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_canAct) return;

            var direction = GetMovementDirection();
            _playerMovement.UpdateMovement(direction);
            _playerRotation.UpdateRotation(direction);
            _playerAnimation.UpdateAnimationViaInputValues(_playerInput.MovementInput);
        }

        public void DisableMotion()
        {
            _canAct = false;
            _playerMovement.DisableCharacterControllerComponent();
        }

        private Vector3 GetMovementDirection()
        {
            var cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f,
                    _playerCamera.transform.forward.z)
                .normalized;
            var cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f,
                    _playerCamera.transform.right.z)
                .normalized;
            var movementDirection = cameraRightXZ * _playerInput.MovementInput.x +
                                    cameraForwardXZ * _playerInput.MovementInput.y;
            return movementDirection;
        }
    }
}