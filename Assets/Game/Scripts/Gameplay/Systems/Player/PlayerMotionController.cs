using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerMotionController : Listeners.IUpdateListener
    {
        private readonly PlayerLocomotionInput _playerLocomotionInput;
        private readonly Camera _playerCamera;
        private readonly PlayerAnimation _playerAnimation;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerRotation _playerRotation;

        public PlayerMotionController(PlayerAnimation playerAnimation, PlayerMovement playerMovement,
            PlayerRotation playerRotation, Camera playerCamera, PlayerLocomotionInput playerLocomotionInput)
        {
            _playerLocomotionInput = playerLocomotionInput;
            _playerAnimation = playerAnimation;
            _playerMovement = playerMovement;
            _playerRotation = playerRotation;
            _playerCamera = playerCamera;
        }

        public void OnUpdate(float deltaTime)
        {
            var direction = GetMovementDirection();
            _playerMovement.UpdateMovement(direction);
            _playerRotation.UpdateRotation(direction);
            _playerAnimation.UpdateAnimationViaInputValues(_playerLocomotionInput.MovementInput);
        }

        private Vector3 GetMovementDirection()
        {
            var cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f,
                    _playerCamera.transform.forward.z)
                .normalized;
            var cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f,
                    _playerCamera.transform.right.z)
                .normalized;
            var movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x +
                                    cameraForwardXZ * _playerLocomotionInput.MovementInput.y;
            return movementDirection;
        }
    }
}