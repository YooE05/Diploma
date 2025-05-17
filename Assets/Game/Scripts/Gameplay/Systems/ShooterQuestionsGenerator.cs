using System;
using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterQuestionsGenerator : MonoBehaviour
    {
        public event Action OnGoodAnswer;
        public event Action OnBadAnswer;

        private DialogueState _dialogueState;

        // [SerializeField] private DialogueGroupsSequenceConfig _questions;
        [SerializeField] private List<DSDialogueSO> _questions;
        [SerializeField] private List<DSDialogueSO> _goodAnswers;
        [SerializeField] private List<DSDialogueSO> _badAnswers;

        [Inject]
        public void Construct(DialogueState dialogueState)
        {
            _dialogueState = dialogueState;

            _dialogueState.OnDialogueStart += OnDialogueStart;
            _dialogueState.OnDialogueFinished += OnDialogueFinish;
        }

        private void OnDialogueStart(DSDialogueSO dialogue)
        {
            if (_goodAnswers.Contains(dialogue))
            {
                ShowGoodAnswer();
            }

            if (_badAnswers.Contains(dialogue))
            {
                ShowBadAnswer();
            }
        }

        private void OnDialogueFinish(DSDialogueSO dialogue)
        {
            if (_goodAnswers.Contains(dialogue))
                CloseGoodAnswer();

            if (_badAnswers.Contains(dialogue))
                CloseBadAnswer();
        }

        public void ShowQuestion()
        {
            _dialogueState.StartDialogue(_questions.GetRandomItemsFisherYates(1)[0]);
        }

        private void ShowBadAnswer()
        {
            //_dialogueState.StartDialogue(_badAnswers.GetRandomItemsFisherYates(1)[0]);
        }

        private void ShowGoodAnswer()
        {
            //_dialogueState.StartDialogue(_badAnswers.GetRandomItemsFisherYates(1)[0]);
        }

        private void CloseBadAnswer()
        {
            OnBadAnswer?.Invoke();
        }

        private void CloseGoodAnswer()
        {
            OnGoodAnswer?.Invoke();
        }
    }
}