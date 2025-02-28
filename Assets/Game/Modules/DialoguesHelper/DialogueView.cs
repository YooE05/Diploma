using System.Collections.Generic;
using DS.Data;
using DS.ScriptableObjects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using YooE;

public sealed class DialogueView : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private ButtonView _continueButton;

    [SerializeField] private List<ChoiceButton> _choiceButtons = new();

    [SerializeField] private DialoguePresenter _dialoguePresenter;

    private void OnEnable()
    {
        _continueButton.OnButtonClicked += ContinueDialogue;

        _dialoguePresenter.OnNewDialogueSet += InitDialogueView;
        _dialoguePresenter.OnDialogueGroupFinished += HideDialogueGroupPanel;

        for (var i = 0; i < _choiceButtons.Count; i++)
        {
            _choiceButtons[i].OnChoiceDone += ChoiceDoneActions;
        }
    }

    private void OnDisable()
    {
        _continueButton.OnButtonClicked -= ContinueDialogue;

        _dialoguePresenter.OnNewDialogueSet -= InitDialogueView;
        _dialoguePresenter.OnDialogueGroupFinished -= HideDialogueGroupPanel;

        for (var i = 0; i < _choiceButtons.Count; i++)
        {
            _choiceButtons[i].OnChoiceDone -= ChoiceDoneActions;
        }
    }

    [Button]
    public void HideDialogueGroupPanel()
    {
        _dialoguePanel.SetActive(false);
    }

    [Button]
    private void ShowDialoguePanel()
    {
        _dialoguePanel.SetActive(true);
    }

    private void InitDialogueView(DSDialogueSO dialogue)
    {
        _dialogueText.text = $"{dialogue.Text}";
        _characterNameText.text = $"{dialogue.CharacterName}";
        SetupChoiceButtons(dialogue.Choices);

        ShowDialoguePanel();
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

        _dialoguePresenter.SetNextDialogueValue(nextDialog);
        ContinueDialogue();
    }

    [Button]
    public void ContinueDialogue()
    {
        _dialoguePresenter.ContinueDialogue();
    }
}