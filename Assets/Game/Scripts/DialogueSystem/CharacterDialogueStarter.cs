using DS.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class CharacterDialogueStarter : MonoBehaviour
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

        [Button]
        public void StartDialogueGroup(DSDialogueGroupSO groupToStart)
        {
            _dialogueState.StartDialogueGroup(groupToStart);
            _dialogueState.OnDialogueGroupFinished += SetNextDialogueGroup;
        }

        [Button]
        public void SetNextDialogueGroup()
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