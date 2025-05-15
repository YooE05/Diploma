using UnityEngine;

namespace YooE.Diploma
{
    public class PlayerMovement
    {
        private readonly CharacterController _characterController;
        private readonly PlayerMovementConfig _playerMovementConfig;
        private readonly PlayerState _playerState;

        public PlayerMovement(CharacterController characterController, PlayerMovementConfig playerMovementConfig)
        {
            _characterController = characterController;
            _playerMovementConfig = playerMovementConfig;
            _playerState = new PlayerState();
        }

        public void UpdateMovement(Vector3 moveDirection, float deltaTime)
        {
            var movementDelta = moveDirection * _playerMovementConfig.RunAcceleration * deltaTime;
            var newVelocity = _characterController.velocity + movementDelta;

            var currentDrag = newVelocity.normalized * _playerMovementConfig.Drag * deltaTime;
            newVelocity = (newVelocity.magnitude > _playerMovementConfig.Drag * deltaTime)
                ? newVelocity - currentDrag
                : Vector3.zero;
            newVelocity = Vector3.ClampMagnitude(newVelocity, _playerMovementConfig.RunSpeed);

            newVelocity.y = _playerMovementConfig.YVelocity;

            _characterController.Move(newVelocity * deltaTime);

            UpdateMovementState(moveDirection);
        }

        public void DisableCharacterControllerComponent()
        {
            _characterController.enabled = false;
        }

        public void EnableCharacterControllerComponent()
        {
            _characterController.enabled = true;
        }

        private void UpdateMovementState(Vector3 inputDirection)
        {
            var isGettingInput = inputDirection != Vector3.zero;
            var hasVelocity = HasVelocity();

            var state = isGettingInput || hasVelocity ? PlayerMovementState.Running : PlayerMovementState.Idling;
            _playerState.SetMovementState(state);
        }

        private bool HasVelocity()
        {
            var velocity = new Vector3(_characterController.velocity.x, 0f,
                _characterController.velocity.z);
            return velocity.magnitude > _playerMovementConfig.MovingThreshold;
        }
    }
}