using UnityEngine;
using UnityEngine.InputSystem;

namespace YooE.Diploma
{
    public sealed class PlayerInput : PlayerControls.IPlayerLocomotionMapActions, Listeners.IInitListener,
        Listeners.IFinishListener
    {
        private PlayerControls PlayerControls { get; set; }
        public Vector2 MovementInput { get; private set; }

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

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }
    }
}