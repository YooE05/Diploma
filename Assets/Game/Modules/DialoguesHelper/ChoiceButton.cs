using System;
using DS.Data;
using DS.ScriptableObjects;
using YooE;

public sealed class ChoiceButton : SwitchButtonViewWithText
{
    public event Action<DSDialogueSO> OnChoiceDone;

    private DSDialogueChoiceData _choiceData;

    public void SetUpAndShow(DSDialogueChoiceData choiceData)
    {
        _choiceData = choiceData;
        SetText(_choiceData.Text);
        Reset();

        OnButtonClicked += DoChoice;
        
        Show();
    }

    private void DoChoice()
    {
        SetSwitchPosition(false);
        OnButtonClicked -= DoChoice;
        OnChoiceDone?.Invoke(_choiceData.NextDialogue);
    }

    public void Reset()
    {
        EnableButton();
        SetSwitchPosition(true);
        Hide();
    }
}