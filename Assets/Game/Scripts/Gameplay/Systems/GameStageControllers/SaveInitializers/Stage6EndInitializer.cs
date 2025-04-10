using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage6EndInitializer : StageInitializer
    {
        private CharactersDataHandler _charactersDataHandler;

        [Inject]
        public void Construct(CharactersDataHandler charactersDataHandler)
        {
            _charactersDataHandler = charactersDataHandler;
        }

        public override void InitGameView()
        {
            _charactersTransform.MovePlayerToNPC();
            _charactersDataHandler.GetCharacterDialogueComponent(DialogueCharacterID.MainScientist)
                .StartCurrentDialogueGroup().Forget();
        }
    }
}