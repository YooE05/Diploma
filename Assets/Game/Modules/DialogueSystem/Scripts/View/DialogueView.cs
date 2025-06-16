using System;
using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private GameObject _answersPanel;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private ButtonView _continueButton;

        [SerializeField] private List<ChoiceButton> _choiceButtons = new();

        private DialogueState _dialogueState;
        private bool _isButtonsSubscribed;

        private int _totalCharsInLine;
        private Tween _typewriteTween;

        [Inject]
        public void Construct(DialogueState dialogueState)
        {
            _dialogueState = dialogueState;

            _dialogueState.OnDialogueStart += InitDialogueView;
            _dialogueState.OnDialogueGroupFinished += Hide;

            HideNoAnimation();
        }

        private void Show()
        {
            _dialoguePanel.transform.DOLocalMoveX(-1924.63f, 0f).Play().SetLink(_dialoguePanel);
            _dialoguePanel.transform.DOLocalMoveX(-8f, 0.3f).Play().SetLink(_dialoguePanel);
            _dialoguePanel.SetActive(true);
            _continueButton.ClearListeners();
            _continueButton.OnButtonClicked += ContinueDialogue;
        }

        private void Hide()
        {
            _dialoguePanel.transform.DOLocalMoveX(-1924.63f, 0.2f).Play().SetLink(_dialoguePanel)
                .OnComplete(() => { _dialoguePanel.SetActive(false); });

            _continueButton.ClearListeners();
            _continueButton.OnButtonClicked -= ContinueDialogue;
        }

        private void HideNoAnimation()
        {
            _dialoguePanel.SetActive(false);
            _continueButton.ClearListeners();
            _continueButton.OnButtonClicked -= ContinueDialogue;
        }

        private void InitDialogueView(DSDialogueSO dialogue)
        {
            _typewriteTween.Complete();
            _dialogueText.text = (dialogue.Text);
            _dialogueText.maxVisibleCharacters = 0;

            _totalCharsInLine = dialogue.Text.Length;

            _typewriteTween = DOTween.To(
                () => _dialogueText.maxVisibleCharacters,
                x => _dialogueText.maxVisibleCharacters = x,
                _totalCharsInLine,
                (1.6f / 104f * _totalCharsInLine)
            ).SetEase(Ease.Linear);
            _typewriteTween.Play();

            // _dialogueText.text = $"{dialogue.Text}";
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
                // _choiceButtons[i].OnChoiceDone = (DSDialogueChoiceData d) => { };
            }

            SubscribeUnsubscribeOnButtons(false);

            if (choices.Count == 1)
            {
                _answersPanel.SetActive(false);
                _continueButton.Show();
                _continueButton.transform.DOLocalMoveX(859.19f, 0.6f).From(887f).SetLoops(-1, LoopType.Yoyo)
                    .SetLink(_continueButton.gameObject).Play();
            }
            else
            {
                _answersPanel.SetActive(true);
                if (!_answersPanel.activeSelf)
                {
                    _answersPanel.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.Unset).SetLink(_answersPanel)
                        .Play();
                }

                _continueButton.Hide();

                for (var i = 0; i < choices.Count; i++)
                {
                    _choiceButtons[i].SetUpAndShow(choices[i]);
                    _choiceButtons[i].AnimateWithDelay(0.3f + 0.15f * i);
                }

                SubscribeUnsubscribeOnButtons(true);
            }
        }

        private void SubscribeUnsubscribeOnButtons(bool needSubscribe)
        {
            if (needSubscribe)
            {
                if (!_isButtonsSubscribed)
                {
                    for (var i = 0; i < _choiceButtons.Count; i++)
                    {
                        _choiceButtons[i].OnChoiceDone += ChoiceDoneActions;
                    }
                }
                else
                {
                    Debug.LogWarning("Repeated buttonsSubscribe");
                }
            }
            else
            {
                if (_isButtonsSubscribed)
                {
                    for (var i = 0; i < _choiceButtons.Count; i++)
                    {
                        _choiceButtons[i].OnChoiceDone -= ChoiceDoneActions;
                    }
                }
                else
                {
                    Debug.LogWarning("Repeated buttons Unsubscribe");
                }
            }

            _isButtonsSubscribed = needSubscribe;
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
            _typewriteTween?.Complete();
            _dialogueState.OnDialogueStart -= InitDialogueView;
            _dialogueState.OnDialogueGroupFinished -= Hide;
        }
    }
}