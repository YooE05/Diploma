using DS.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class DialogueStarter : MonoBehaviour
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private DSDialogueGroupSO _group;
        private DialogueState _dialogueState;

        [Inject]
        public void Construct(DialogueState dialogueState)
        {
            _dialogueState = dialogueState;
            _dialogueView.SetDialogueState(_dialogueState);
            _dialogueView.Hide();
        }

        [Button]
        public void ShowDialoguePanel()
        {
            _dialogueState.StartDialogueGroup(_group);
        }

        [Button]
        public void ShowDialoguePanel(DSDialogueGroupSO groupToStart)
        {
            _dialogueState.StartDialogueGroup(groupToStart);
        }
    }
}