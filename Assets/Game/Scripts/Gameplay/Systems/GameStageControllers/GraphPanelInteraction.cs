using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;
using UnityEngine.UI;

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

        [SerializeField] private GameObject _graphPoint1;
        [SerializeField] private GameObject _graphPoint2;
        [SerializeField] private GameObject _graphPoint3;

        [SerializeField] private GameObject _graphLine;
        [SerializeField] private Image _graphLineImage;

        [SerializeField] private GameObject _littleGraphGO;

        private int _clickedButtonsCount;
        private CharactersDataHandler _charactersDataHandler;
        private CharacterDialogueComponent _mainNPC;

        public void OnInit()
        {
            _panelGO.SetActive(false);
            _littleGraphGO.SetActive(false);
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

            _graphPoint1.SetActive(false);
            _graphPoint2.SetActive(false);
            _graphPoint3.SetActive(false);
            _graphLine.SetActive(false);

            _panelGO.SetActive(true);
            ShowTutorialHand();
        }

        public void ShowLittleGraph()
        {
            _littleGraphGO.SetActive(true);
        }

        public void HideLittleGraph()
        {
            _littleGraphGO.SetActive(false);
            _littleGraphGO.transform.DOScaleY(0f, 0.5f);
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
            _notepadButton1.OnButtonClicked -= Button1Click;
            _notepadButton1.transform.DOScale(0f, 0.5f).SetLink(_notepadButton1.gameObject).OnComplete(() =>
            {
                _notepadButton1.DisableButton();
            }).Play();

            _graphPoint1.transform.DOScale(1f, 0.3f).From(0f).SetLink(_graphPoint1).Play();
            _graphPoint1.SetActive(true);
            _clickedButtonsCount++;
            CheckIsAllButtonsClick();
            HideTutorialHand();
        }

        private void Button2Click()
        {
            _notepadButton2.OnButtonClicked -= Button2Click;
            _notepadButton2.transform.DOScale(0f, 0.5f).SetLink(_notepadButton2.gameObject).OnComplete(() =>
            {
                _notepadButton2.DisableButton();
            }).Play();

            _graphPoint2.transform.DOScale(1f, 0.3f).From(0f).SetLink(_graphPoint1).Play();
            _graphPoint2.SetActive(true);
            _clickedButtonsCount++;
            CheckIsAllButtonsClick();
            HideTutorialHand();
        }

        private void Button3Click()
        {
            _notepadButton3.OnButtonClicked -= Button2Click;
            _notepadButton3.transform.DOScale(0f, 0.5f).SetLink(_notepadButton3.gameObject).OnComplete(() =>
            {
                _notepadButton3.DisableButton();
            }).Play();

            _graphPoint3.transform.DOScale(1f, 0.3f).From(0f).SetLink(_graphPoint1).Play();
            _graphPoint3.SetActive(true);
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
            _graphLineImage.DOFade(1f, 2.5f).From(0).SetLink(_graphLine).Play();
            _graphLine.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: CancellationToken.None);
            Hide();
            ShowLittleGraph();
            GoNextMainDialogue();
        }
    }
}