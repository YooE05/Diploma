using System;
using System.Linq;
using DS.Enumerations;
using DS.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class DialoguePresenter : MonoBehaviour
{
    public event Action<DSDialogueSO> OnNewDialogueSet;
    //public event Action<DSDialogueSO> OnDialoguePartFinished;

    public event Action OnDialogueGroupFinished;

    [SerializeField] private DSDialogueContainerSO _dialogueContainer;

    private DSDialogueGroupSO _currentGroup;

    private DSDialogueSO _currentDialogue;
    private DSDialogueSO _nextDialogue;

    [Button]
    public void InitDialogueGroup(string groupName)
    {
        _currentGroup = _dialogueContainer.DialogueGroups.Keys.FirstOrDefault(k => k.GroupName == groupName);

        if (_currentGroup == null)
        {
            Debug.LogError("Incorrect Group Name");
            return;
        }

        _currentDialogue = _dialogueContainer.DialogueGroups[_currentGroup][0];
        if (_currentDialogue.DialogueType == DSDialogueType.SingleChoice)
        {
            _nextDialogue = _currentDialogue.Choices[0].NextDialogue;
        }

        OnNewDialogueSet?.Invoke(_currentDialogue);
    }

    public void ContinueDialogue()
    {
        if (_nextDialogue == null)
        {
            OnDialogueGroupFinished?.Invoke();
            return;
        }

        //  OnDialoguePartFinished
        _currentDialogue = _nextDialogue;
        _nextDialogue = null;
        OnNewDialogueSet?.Invoke(_currentDialogue);
    }

    public void SetNextDialogueValue(DSDialogueSO next)
    {
        _nextDialogue = next;
    }
}