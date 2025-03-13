using System;
using System.Collections.Generic;
using DS.Data;
using DS.ScriptableObjects;
using TMPro;
using UnityEngine;
using Zenject;

namespace YooE.DialogueSystem
{
    public sealed class DialogueView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private ButtonView _continueButton;

        [SerializeField] private List<ChoiceButton> _choiceButtons = new();

        private DialogueState _dialogueState;

        [Inject]
        public void Construct(DialogueState dialogueState)
        {
            _dialogueState = dialogueState;

            _dialogueState.OnDialogueStart += InitDialogueView;
            _dialogueState.OnDialogueGroupFinished += Hide;

            for (var i = 0; i < _choiceButtons.Count; i++)
            {
                _choiceButtons[i].OnChoiceDone += ChoiceDoneActions;
            }

            Hide();
        }

        private void Show()
        {
            _dialoguePanel.SetActive(true);
            _continueButton.ClearListeners();
            _continueButton.OnButtonClicked += ContinueDialogue;
        }

        private void Hide()
        {
            _dialoguePanel.SetActive(false);
            _continueButton.ClearListeners();
            _continueButton.OnButtonClicked -= ContinueDialogue;
        }

        private void InitDialogueView(DSDialogueSO dialogue)
        {
            _dialogueText.text = $"{dialogue.Text}";
            _characterNameText.text = $"{dialogue.CharacterName}";
            SetupChoiceButtons(dialogue.Choices);

            if (dialogue.IsStartingDialogue)
            {
                Show();
            }
        }

        private void SetupChoiceButtons(List<DSDialogueChoiceData> choices)
        {
            for (var i = 0; i < _choiceButtons.Count; i++)
            {
                _choiceButtons[i].Reset();
            }

            if (choices.Count == 1)
            {
                _continueButton.Show();
            }
            else
            {
                _continueButton.Hide();

                for (var i = 0; i < choices.Count; i++)
                {
                    _choiceButtons[i].SetUpAndShow(choices[i]);
                }
            }
        }

        private void ChoiceDoneActions(DSDialogueSO nextDialog)
        {
            for (var i = 0; i < _choiceButtons.Count; i++)
            {
                _choiceButtons[i].DisableButton();
            }

            _dialogueState.SetNextDialogueValue(nextDialog);
            ContinueDialogue();
        }

        private void ContinueDialogue()
        {
            _dialogueState.ContinueDialogue();
        }

        public void Dispose()
        {
            _dialogueState.OnDialogueStart -= InitDialogueView;
            _dialogueState.OnDialogueGroupFinished -= Hide;
        }
    }
}