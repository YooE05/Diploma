using System;
using Cysharp.Threading.Tasks;
using DS.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace YooE.DialogueSystem
{
    public sealed class CharacterDialogueComponent : MonoBehaviour
    {
        [SerializeField] private DialogueGroupsSequenceConfig _dialogueSequence;
        [SerializeField] private int _currentGroupIndex = 0;
        [SerializeField] private DialogueCharacterID _dialogueCharacterID;

        private DialogueState _dialogueState;
        private DSDialogueGroupSO CurrentDialogueGroup => _dialogueSequence.Groups[_currentGroupIndex];

        [Inject]
        public void Construct(DialogueState dialogueState)
        {
            _dialogueState = dialogueState;
        }

        public void SetGroupIndex(int newGroupIndex)
        {
            if (_dialogueSequence.Groups.Count == 0)
            {
                Debug.LogError("No dialogue groups for character");
                return;
            }

            var indexOfLastGroup = _dialogueSequence.Groups.Count - 1;
            _currentGroupIndex = indexOfLastGroup < newGroupIndex ? indexOfLastGroup : newGroupIndex;
        }

        public CharacterDialogueData GetCharacterData()
        {
            var data = new CharacterDialogueData()
            {
                //DialogueCharacterID = nameof(_dialogueCharacterID),
                DialogueCharacterID = Enum.GetName(typeof(DialogueCharacterID), _dialogueCharacterID),
                GroupIndex = _currentGroupIndex,
            };

            return data;
        }

        [Button]
        public async UniTaskVoid StartCurrentDialogueGroup()
        {
            await UniTask.WaitUntil(() => _dialogueState != null);

            StartDialogueGroup(CurrentDialogueGroup);
        }

        private void StartDialogueGroup(DSDialogueGroupSO groupToStart)
        {
            _dialogueState.StartDialogueGroup(groupToStart);
        }

        public void SetNextDialogueGroup()
        {
            if (_dialogueSequence.Groups.Count == 0)
            {
                Debug.LogError("No dialogue groups for character");
                return;
            }

            if (_dialogueSequence.Groups.Count > _currentGroupIndex + 1)
            {
                _currentGroupIndex++;
            }
            else
            {
                Debug.Log("There is no more character dialogues!");
            }
        }

        private void OnDestroy()
        {
            //  _dialogueState.OnDialogueGroupFinished? -= SetNextDialogueGroup;
        }
    }
}