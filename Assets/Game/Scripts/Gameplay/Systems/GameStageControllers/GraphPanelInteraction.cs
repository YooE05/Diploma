using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class GraphPanelInteraction : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _panelGO;

        [SerializeField] private ButtonView _notepadButton1;
        [SerializeField] private ButtonView _notepadButton2;
        [SerializeField] private ButtonView _notepadButton3;

        [SerializeField] private GameObject _tutorialHand;
        private Tween _handAnimation;

        private int _clickedButtonsCount;
        private CharactersDataHandler _charactersDataHandler;
        private CharacterDialogueComponent _mainNPC;

        public void OnInit()
        {
            _panelGO.SetActive(false);
        }

        [Inject]
        public void Construct(CharactersDataHandler charactersDataHandler)
        {
            _charactersDataHandler = charactersDataHandler;
            _mainNPC = _charactersDataHandler.GetCharacterDialogueComponent(DialogueCharacterID.MainScientist);
        }

        private void GoNextMainDialogue()
        {
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);
            _mainNPC.StartCurrentDialogueGroup().Forget();
        }

        [Button]
        public void Show()
        {
            _clickedButtonsCount = 0;
            _notepadButton1.OnButtonClicked += Button1Click;
            _notepadButton2.OnButtonClicked += Button2Click;
            _notepadButton3.OnButtonClicked += Button3Click;

            _panelGO.SetActive(true);
            ShowTutorialHand();
        }

        [Button]
        public void Hide()
        {
            _panelGO.SetActive(false);
            _notepadButton1.OnButtonClicked -= Button1Click;
            _notepadButton2.OnButtonClicked -= Button2Click;
            _notepadButton3.OnButtonClicked -= Button3Click;

            HideTutorialHand();
        }

        private void ShowTutorialHand()
        {
            _tutorialHand.SetActive(true);
            _handAnimation = _tutorialHand.transform.DOScaleY(0.73f, 0.5f).From(1f).SetLoops(-1, LoopType.Yoyo);
            _handAnimation.Restart();
        }

        private void HideTutorialHand()
        {
            _handAnimation.Kill();
            _tutorialHand.SetActive(false);
        }

        private void Button1Click()
        {
            _notepadButton1.DisableButton();
            _notepadButton1.OnButtonClicked -= Button1Click;
            //play Animation
            _clickedButtonsCount++;
            CheckIsAllButtonsClick();
            HideTutorialHand();
        }

        private void Button2Click()
        {
            _notepadButton2.DisableButton();
            _notepadButton2.OnButtonClicked -= Button2Click;
            //play Animation
            _clickedButtonsCount++;
            CheckIsAllButtonsClick();
            HideTutorialHand();
        }

        private void Button3Click()
        {
            _notepadButton3.DisableButton();
            _notepadButton3.OnButtonClicked -= Button3Click;
            //play Animation
            _clickedButtonsCount++;
            CheckIsAllButtonsClick();
            HideTutorialHand();
        }

        private void CheckIsAllButtonsClick()
        {
            if (_clickedButtonsCount == 3)
            {
                ShowGraphAnimation().Forget();
            }
        }

        private async UniTaskVoid ShowGraphAnimation()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: CancellationToken.None);
            Hide();
            GoNextMainDialogue();
        }
    }
}