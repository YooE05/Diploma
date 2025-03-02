using System;
using System.Collections.Generic;
using DS.Enumerations;
using DS.ScriptableObjects;
using UnityEngine;

namespace YooE.DialogueSystem
{
    public sealed class DialogueState
    {
        public event Action<DSDialogueSO> OnDialogueStart;
        public event Action<DSDialogueSO> OnDialogueFinished;
        public event Action OnDialogueGroupFinished;

        private readonly DSDialogueContainerSO _dialogueContainer;

        private DSDialogueGroupSO _currentGroup;
        private List<DSDialogueSO> _groupDialoguesList;

        private DSDialogueSO _currentDialogue;
        private DSDialogueSO _nextDialogue;

        public DialogueState(DSDialogueContainerSO dialogueContainer)
        {
            _dialogueContainer = dialogueContainer;
        }

        public void StartDialogueGroup(string groupName)
        {
            _currentGroup = _dialogueContainer.GetDialogueGroup(groupName);
            _groupDialoguesList = _dialogueContainer.DialogueGroups[_currentGroup];

            if (_currentGroup == null)
            {
                Debug.LogError("Incorrect Group Name");
                return;
            }

            StartDialogue(_groupDialoguesList[0]);
        }

        public void StartDialogueGroup(DSDialogueGroupSO group)
        {
            _currentGroup = group;
            _groupDialoguesList = _dialogueContainer.DialogueGroups[_currentGroup];

            if (_currentGroup == null)
            {
                Debug.LogError("Incorrect Group");
                return;
            }

            StartDialogue(_groupDialoguesList[0]);
        }

        private void StartDialogue(DSDialogueSO dialogue)
        {
            _currentDialogue = dialogue;
            if (_currentDialogue.DialogueType == DSDialogueType.SingleChoice)
            {
                _nextDialogue = _currentDialogue.Choices[0].NextDialogue;
            }

            OnDialogueStart?.Invoke(_currentDialogue);
        }

        public void ContinueDialogue()
        {
            OnDialogueFinished?.Invoke(_currentDialogue);

            if (_nextDialogue == null)
            {
                OnDialogueGroupFinished?.Invoke();
                return;
            }

            _currentDialogue = _nextDialogue;
            _nextDialogue = null;
            OnDialogueStart?.Invoke(_currentDialogue);
        }

        public void SetNextDialogueValue(DSDialogueSO next)
        {
            _nextDialogue = next;
        }
    }
}