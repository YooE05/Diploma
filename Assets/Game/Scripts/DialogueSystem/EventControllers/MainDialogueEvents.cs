using System;
using System.Collections.Generic;
using System.Threading;
using Audio;
using Cysharp.Threading.Tasks;
using DS.ScriptableObjects;
using YooE.DialogueSystem;
using YooE.Diploma.Interaction;
using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class StartShooterEvent : DialogueEvent
    {
        private readonly CubeHandler _cubeHandler;
        private readonly ScienceBaseGameController _scienceBaseGameController;

        public StartShooterEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceBaseGameController scienceBaseGameController) :
            base(dialogueState, dialogues)
        {
            _scienceBaseGameController = scienceBaseGameController;
        }

        protected override void FinishActions()
        {
            AsyncCountdown(1f, CancellationToken.None).Forget();
        }

        private async UniTask AsyncCountdown(float countdown, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(countdown), cancellationToken: token);

            _scienceBaseGameController.GoToShooterScene();
        }
    }

    public sealed class EnableFightDoorEvent : DialogueEvent
    {
        private readonly FightDoorInteractionComponent _fightDoor;

        public EnableFightDoorEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            FightDoorInteractionComponent fightDoor) :
            base(dialogueState, dialogues)
        {
            _fightDoor = fightDoor;
        }

        protected override void FinishActions()
        {
            _fightDoor.EnableInteractionAbility();
        }
    }

    public sealed class EnableMotionEvent : DialogueEvent
    {
        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerInteraction _playerInteraction;

        public EnableMotionEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            PlayerMotionController playerMotionController, PlayerInteraction playerInteraction) :
            base(dialogueState, dialogues)
        {
            _playerMotionController = playerMotionController;
            _playerInteraction = playerInteraction;
        }

        protected override void FinishActions()
        {
            _playerMotionController.EnableMotion();
            _playerInteraction.EnableInteraction();
        }
    }

    public sealed class GoNextMainDialogueEvent : DialogueEvent
    {
        private readonly CharactersDataHandler _charactersDataHandler;

        public GoNextMainDialogueEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            CharactersDataHandler charactersDataHandler) :
            base(dialogueState, dialogues)
        {
            _charactersDataHandler = charactersDataHandler;
        }

        protected override void FinishActions()
        {
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);
        }
    }

    public sealed class CompleteGameEvent : DialogueEvent
    {
        private readonly CharactersDataHandler _charactersDataHandler;
        private readonly PlayerDataContainer _playerDataContainer;
        private readonly StoreManager _storeManager;
        private readonly CharacterDialogueComponent _mainNPC;
        private readonly AudioManager _audioManager;
        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerInteraction _playerInteraction;

        public CompleteGameEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            CharactersDataHandler charactersDataHandler, PlayerDataContainer playerDataContainer,
            StoreManager storeManager, CharacterDialogueComponent mainNPC, AudioManager audioManager,
            PlayerMotionController playerMotionController, PlayerInteraction playerInteraction) :
            base(dialogueState, dialogues)
        {
            _playerMotionController = playerMotionController;
            _playerInteraction = playerInteraction;

            _audioManager = audioManager;
            _mainNPC = mainNPC;
            _charactersDataHandler = charactersDataHandler;
            _playerDataContainer = playerDataContainer;
            _storeManager = storeManager;
        }

        protected override void FinishActions()
        {
            /*_charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);*/

            if (_audioManager.TryGetAudioClipByName("completeGame", out var audioClip))
            {
                _audioManager.PlaySoundOneShot(audioClip, AudioOutput.Music);
            }

            DelayStartDialogue().Forget();
        }

        private async UniTaskVoid DelayStartDialogue()
        {
            await UniTask.WaitForSeconds(2.5f);

            _playerMotionController.DisableMotion();
            _playerInteraction.DisableInteraction();

            _mainNPC.StartCurrentDialogueGroup().Forget();
            _playerDataContainer.IsGameCompleted = true;
            _storeManager.OnStart();
        }
    }

    public sealed class HideTaskPanelEvent : DialogueEvent
    {
        private readonly TaskPanel _taskPanel;

        public HideTaskPanelEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            TaskPanel taskPanel) :
            base(dialogueState, dialogues)
        {
            _taskPanel = taskPanel;
        }

        protected override void StartActions()
        {
            _taskPanel.Hide();
        }
    }

    public sealed class SaveGameEvent : DialogueEvent
    {
        private readonly SaveLoadManager _saveLoadManager;

        public SaveGameEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            SaveLoadManager saveLoadManager) :
            base(dialogueState, dialogues)
        {
            _saveLoadManager = saveLoadManager;
        }

        protected override void FinishActions()
        {
            _saveLoadManager.SaveGame();
        }
    }
}