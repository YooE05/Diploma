using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class StoreInteractionComponent : InteractionComponent
    {
        [Inject] private PlayerMotionController _playerMotionController;
        [Inject] private PlayerInteraction _playerInteraction;
        [Inject] private StoreManager _storeManager;

        public override void Interact()
        {
            base.Interact();

            _storeManager.OpenStore();
            _playerMotionController.DisableMotion();
            _playerInteraction.DisableInteraction();

            _storeManager.OnStoreClosed += EnablePlayer;
        }

        private void EnablePlayer()
        {
            _storeManager.OnStoreClosed -= EnablePlayer;
            _playerMotionController.EnableMotion();
            _playerInteraction.EnableInteraction();
        }
    }
}