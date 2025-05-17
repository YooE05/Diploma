using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class PlayerInput : PlayerControls.IPlayerLocomotionMapActions, Listeners.IInitListener,
        Listeners.IFinishListener
    {
        private PlayerControls.IPlayerLocomotionMapActions _playerLocomotionMapActionsImplementation;
        public PlayerControls PlayerControls { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public Vector2 LastPointerScreenPosition { get; private set; }
        public bool IsPointerPressed { get; private set; }

        public void OnInit()
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();

            PlayerControls.PlayerLocomotionMap.Enable();
            PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        public void OnFinish()
        {
            PlayerControls.PlayerLocomotionMap.Disable();
            PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        public void OnPointerPosition(InputAction.CallbackContext context)
        {
            LastPointerScreenPosition = context.ReadValue<Vector2>();
        }

        public void OnPointerInteractionAbility(InputAction.CallbackContext context)
        {
            IsPointerPressed = context.ReadValueAsButton();
        }
    }

    public sealed class PlayerShooterInput : PlayerControls.IShooterMapActions, Listeners.IStartListener,
        Listeners.IFinishListener
    {
        public event Action OnFirstWeaponPicked;
        public event Action OnSecondWeaponPicked;
        public event Action OnThirdWeaponPicked;

        private PlayerControls.IPlayerLocomotionMapActions _playerLocomotionMapActionsImplementation;
        private PlayerControls PlayerControls { get; set; }

        [Inject] private PlayerInput _playerMoveInput;

        public void OnStart()
        {
            PlayerControls = _playerMoveInput.PlayerControls;

            PlayerControls.ShooterMap.Enable();
            PlayerControls.ShooterMap.SetCallbacks(this);
        }

        public void OnFinish()
        {
            PlayerControls.ShooterMap.Disable();
            PlayerControls.ShooterMap.RemoveCallbacks(this);
        }


        public void OnFirstWeapon(InputAction.CallbackContext context)
        {
            OnFirstWeaponPicked?.Invoke();
        }

        public void OnSecondWeapon(InputAction.CallbackContext context)
        {
            OnSecondWeaponPicked?.Invoke();
        }

        public void OnThirdWeapon(InputAction.CallbackContext context)
        {
            OnThirdWeaponPicked?.Invoke();
        }
    }
}