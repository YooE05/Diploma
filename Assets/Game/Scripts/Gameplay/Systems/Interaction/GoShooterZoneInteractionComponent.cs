using UnityEngine;
using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class GoShooterZoneInteractionComponent : InteractionComponent
    {
        [SerializeField] private ConfirmPopupView _goShooterAskPopup;
        [Inject] private PlayerMotionController _playerMotionController;
        [Inject] private ScienceBaseGameController _gameController;

        public override void OnInit()
        {
            base.OnInit();
            _goShooterAskPopup.Hide();
        }

        public override void Interact()
        {
            base.Interact();

            _playerMotionController.DisableMotion();
            _goShooterAskPopup.Show();
            _goShooterAskPopup.OnDecline += ReturnToPlayerControl;
            _goShooterAskPopup.OnClose += ReturnToPlayerControl;

            _goShooterAskPopup.OnConfirm += StartShooterScene;
        }

        private void ReturnToPlayerControl()
        {
            _goShooterAskPopup.OnDecline -= ReturnToPlayerControl;
            _goShooterAskPopup.OnClose -= ReturnToPlayerControl;
            _goShooterAskPopup.Hide();

            _playerMotionController.EnableMotion();
        }

        private void StartShooterScene()
        {
            _goShooterAskPopup.OnConfirm -= StartShooterScene;
            _gameController.GoToShooterScene();
        }
    }
}