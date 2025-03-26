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
        private readonly Transform _playerTransform;

        private bool _canAct;

        public PlayerMotionController(PlayerAnimation playerAnimation, PlayerMovement playerMovement,
            PlayerRotation playerRotation, Camera playerCamera, PlayerInput playerInput, Transform playerTransform)
        {
            _playerInput = playerInput;
            _playerAnimation = playerAnimation;
            _playerMovement = playerMovement;
            _playerRotation = playerRotation;
            _playerCamera = playerCamera;
            _playerTransform = playerTransform;

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

            var moveOffset = GetMoveOffset();
            var direction = GetMovementDirection(moveOffset);

            _playerMovement.UpdateMovement(direction);
            _playerRotation.UpdateRotation(direction);
            _playerAnimation.UpdateAnimationViaInputValues(moveOffset);
        }

        public void DisableMotion()
        {
            _canAct = false;
            _playerMovement.DisableCharacterControllerComponent();
        }

        private Vector2 GetMoveOffset()
        {
            if (_playerInput.MovementInput != Vector2.zero || !_playerInput.IsPointerInteracting)
                return _playerInput.MovementInput;

            var ray = _playerCamera.ScreenPointToRay(_playerInput.LastPointerScreenPosition);

            //layerMask
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                var offset = hit.point - _playerTransform.position;
                return new Vector2(offset.x, offset.z);
            }

            return Vector2.zero;
        }

        private Vector3 GetMovementDirection(Vector2 moveOffset)
        {
            var cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f,
                    _playerCamera.transform.forward.z)
                .normalized;
            var cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f,
                    _playerCamera.transform.right.z)
                .normalized;
            var movementDirection = cameraRightXZ * moveOffset.x + cameraForwardXZ * moveOffset.y;
            return movementDirection;
        }
    }
}