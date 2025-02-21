using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerRotation
    {
        private readonly float _rotationSpeed;
        private readonly Transform _playerVisual;

        public PlayerRotation(Transform playerVisual, float rotationSpeed)
        {
            _playerVisual = playerVisual;
            _rotationSpeed = rotationSpeed;
        }

        public void UpdateRotation(Vector3 direction)
        {
            if (direction != Vector3.zero)
                MotionUseCases.Rotate(_playerVisual, direction, _rotationSpeed * Time.deltaTime);
        }
    }
}