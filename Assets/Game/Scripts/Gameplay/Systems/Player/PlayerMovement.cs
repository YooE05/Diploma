using UnityEngine;

namespace YooE.Diploma
{
    public class PlayerMovement
    {
        private readonly CharacterController _characterController;
        private readonly PlayerMovementConfig _playerMovementConfig;
        private PlayerState _playerState;

        public PlayerMovement(CharacterController characterController, PlayerMovementConfig playerMovementConfig)
        {
            _characterController = characterController;
            _playerMovementConfig = playerMovementConfig;
            _playerState = new PlayerState();
        }

        public void UpdateMovement(Vector3 moveDirection)
        {
            Vector3 movementDelta = moveDirection * _playerMovementConfig.RunAcceleration * Time.deltaTime;
            Vector3 newVelocity = _characterController.velocity + movementDelta;

            // Add drag to player
            Vector3 currentDrag = newVelocity.normalized * _playerMovementConfig.Drag * Time.deltaTime;
            newVelocity = (newVelocity.magnitude > _playerMovementConfig.Drag * Time.deltaTime)
                ? newVelocity - currentDrag
                : Vector3.zero;
            newVelocity = Vector3.ClampMagnitude(newVelocity, _playerMovementConfig.RunSpeed);

            newVelocity.y = _playerMovementConfig.YVelocity;

            // Move character (Unity suggests only calling this once per tick)
            _characterController.Move(newVelocity * Time.deltaTime);

            UpdateMovementState(moveDirection);
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