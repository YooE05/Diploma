using System;
using DG.Tweening;
using DS.Data;
using DS.ScriptableObjects;

namespace YooE.DialogueSystem
{
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

        public void AnimateWithDelay(float delay)
        {
            transform.DOScale(1, 0.5f).From(0).SetDelay(delay).SetEase(Ease.OutBounce).SetLink(gameObject)
                .Play();
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
}