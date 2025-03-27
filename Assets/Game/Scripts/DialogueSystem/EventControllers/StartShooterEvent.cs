using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DS.ScriptableObjects;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using YooE.DialogueSystem;
using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class StartShooterEvent : DialogueEventController
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

    public sealed class EnableMotionEvent : DialogueEventController
    {
        private readonly PlayerMotionController _playerMotionController;

        public EnableMotionEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            PlayerMotionController playerMotionController) :
            base(dialogueState, dialogues)
        {
            _playerMotionController = playerMotionController;
        }

        protected override void FinishActions()
        {
            _playerMotionController.EnableMotion();
        }
    }

    public sealed class GoNextMainDialogueEvent : DialogueEventController
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

    public sealed class SaveGameEvent : DialogueEventController
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