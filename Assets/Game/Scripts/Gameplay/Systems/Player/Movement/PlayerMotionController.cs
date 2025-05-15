using UnityEngine;
using UnityEngine.EventSystems;

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
        private readonly LayerMask _layerMask;

        private readonly Vector2 _screenCenter;
        public bool СanAct;

        public PlayerMotionController(PlayerAnimation playerAnimation, PlayerMovement playerMovement,
            PlayerRotation playerRotation, Camera playerCamera, PlayerInput playerInput, Transform playerTransform,
            LayerMask layerWithoutPlayer)
        {
            _playerInput = playerInput;
            _playerAnimation = playerAnimation;
            _playerMovement = playerMovement;
            _playerRotation = playerRotation;
            _playerCamera = playerCamera;
            _playerTransform = playerTransform;
            _layerMask = layerWithoutPlayer;

            DisableMotion();
            СanAct = true;

            _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        public void OnStart()
        {
            if (СanAct)
                EnableMotion();
            else
            {
                DisableMotion();
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (!СanAct) return;

            var moveOffset = GetMoveOffset();
            var direction = GetMovementDirection(moveOffset);

            _playerMovement.UpdateMovement(direction, deltaTime);
            _playerRotation.UpdateRotation(direction, deltaTime);
            _playerAnimation.SmoothlyUpdateAnimation(moveOffset, deltaTime);
        }

        public void EnableMotion()
        {
            СanAct = true;
            _playerMovement.EnableCharacterControllerComponent();
        }

        public void DisableMotion()
        {
            СanAct = false;

            _playerAnimation.StopMoveAnimation();
            _playerMovement.DisableCharacterControllerComponent();
        }

        private Vector2 GetMoveOffset()
        {
            if (_playerInput.MovementInput != Vector2.zero || !_playerInput.IsPointerPressed ||
                EventSystem.current.IsPointerOverGameObject())
                return _playerInput.MovementInput;

            /* var ray = _playerCamera.ScreenPointToRay(_playerInput.LastPointerScreenPosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _layerMask))
            {
                Debug.Log(hit.transform.name);
                var offset = hit.point - _playerTransform.position;
                return new Vector2(offset.x, offset.z);
            }*/

            var offset = _playerInput.LastPointerScreenPosition - _screenCenter;
            //  Debug.Log(_playerInput.LastPointerScreenPosition + " " + _screenCenter + " = " + offset + " norm = " + offset.normalized);

            return offset.normalized;
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