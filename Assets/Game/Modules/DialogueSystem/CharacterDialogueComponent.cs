using DS.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace YooE.DialogueSystem
{
    public sealed class CharacterDialogueComponent : MonoBehaviour
    {
        [SerializeField] private DialogueGroupsSequenceConfig _dialogueSequence;
        [SerializeField] private int _currentGroupIndex;

        private DialogueState _dialogueState;
        private DSDialogueGroupSO CurrentDialogueGroup => _dialogueSequence.Groups[_currentGroupIndex];

        [Inject]
        public void Construct(DialogueState dialogueState)
        {
            _dialogueState = dialogueState;
        }

        [Button]
        public void StartCurrentDialogueGroup()
        {
            StartDialogueGroup(CurrentDialogueGroup);
        }

        private void StartDialogueGroup(DSDialogueGroupSO groupToStart)
        {
            _dialogueState.StartDialogueGroup(groupToStart);
            _dialogueState.OnDialogueGroupFinished += SetNextDialogueGroup;
        }

        private void SetNextDialogueGroup()
        {
            _dialogueState.OnDialogueGroupFinished -= SetNextDialogueGroup;
            if (_dialogueSequence.Groups.Count - 1 > _currentGroupIndex)
            {
                _currentGroupIndex++;
            }
            else
            {
                Debug.LogWarning("There is no more character dialogues!");
            }
        }
    }
}