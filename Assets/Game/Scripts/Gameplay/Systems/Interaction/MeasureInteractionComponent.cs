using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class MeasureInteractionComponent : InteractionComponent
    {
        private Stage5TaskTracker _stage5TaskTracker;
        private UIAnimationController _uiAnimationController;
        private CharactersDataHandler _charactersDataHandler;

        private PlayerMotionController _playerMotionController;
        private PlayerInteraction _playerInteraction;


        [Inject]
        public void Construct(Stage5TaskTracker stage5TaskTracker, UIAnimationController uiAnimationController,
            CharactersDataHandler charactersDataHandler, PlayerMotionController playerMotionController,
            PlayerInteraction playerInteraction)
        {
            _stage5TaskTracker = stage5TaskTracker;
            _uiAnimationController = uiAnimationController;
            _charactersDataHandler = charactersDataHandler;

            _playerMotionController = playerMotionController;
            _playerInteraction = playerInteraction;
        }

        public override void Interact()
        {
            base.Interact();
            _uiAnimationController.StartWritingAnimation().Forget();
            _uiAnimationController.OnGetMeasurement += DoneMeasurementActions;

            _playerMotionController.DisableMotion();
            _playerInteraction.DisableInteraction();
        }

        private void DoneMeasurementActions()
        {
            _uiAnimationController.OnGetMeasurement -= DoneMeasurementActions;

            _stage5TaskTracker.MeasurePlants();
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);

            DisableInteractionAbility();
            DisableInteractView();

            _playerMotionController.EnableMotion();
            _playerInteraction.EnableInteraction();
        }
    }
}