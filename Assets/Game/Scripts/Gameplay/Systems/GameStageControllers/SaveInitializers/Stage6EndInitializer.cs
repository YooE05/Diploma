using YooE.DialogueSystem;
using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage6EndInitializer : StageInitializer
    {
        private CharactersDataHandler _charactersDataHandler;
        private GardenViewController _gardenView;
        [Inject] private PlayerMotionController _playerMotionController;
        [Inject] private PlayerInteraction _playerInteraction;

        [Inject]
        public void Construct(CharactersDataHandler charactersDataHandler, GardenViewController gardenView)
        {
            _gardenView = gardenView;
            _charactersDataHandler = charactersDataHandler;
        }

        public override void InitGameView()
        {
            _charactersTransform.MovePlayerToNPC();
            _charactersDataHandler.GetCharacterDialogueComponent(DialogueCharacterID.MainScientist)
                .StartCurrentDialogueGroup().Forget();
            _playerMotionController.СanAct = false;
            _playerInteraction.DisableInteraction();

            _gardenView.ShowEndStageGarden();
        }
    }
}