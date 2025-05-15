using Sirenix.OdinInspector;
using UnityEngine;
using Utils;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class DebugStageInitializer : MonoBehaviour
    {
        private CharactersDataHandler _charactersDataHandler;
        private ScienceBaseGameController _gameController;
        private SaveLoadManager _saveLoadManager;

        [Inject]
        public void Construct(CharactersDataHandler charactersDataHandler, ScienceBaseGameController gameController,
            SaveLoadManager saveLoadManager)
        {
            _charactersDataHandler = charactersDataHandler;
            _gameController = gameController;
            _saveLoadManager = saveLoadManager;
        }

        [Button]
        public void SetupGameMainDialogueIndex(int mainDialogueIndex)
        {
            _charactersDataHandler.SetCharactersDataFromSave(new CharacterDialogueData[]
            {
                new CharacterDialogueData()
                {
                    DialogueCharacterID = EnumUtils<DialogueCharacterID>.ToString(DialogueCharacterID.MainScientist),
                    GroupIndex = mainDialogueIndex
                }
            });
            
            _saveLoadManager.SaveGame();
            _gameController.ReloadScene();
        }
    }
}