using UnityEngine;
using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class FightDoorInteractionComponent : InteractionComponent
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

            DisableInteractView();
            DisableInteractionAbility();

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
            EnableInteractView();
            EnableInteractionAbility();
        }

        private void StartShooterScene()
        {
            _goShooterAskPopup.OnConfirm -= StartShooterScene;
            _gameController.GoToShooterScene();
        }
    }
}