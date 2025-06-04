using DG.Tweening;
using UnityEngine;
using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage1Initializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;
        [Inject] private PlayerMotionController _playerMotionController;
        [Inject] private PlayerInteraction _playerInteraction;

        [SerializeField] private GameObject _tutorialHand;

        public override void InitGameView()
        {
            base.InitGameView();
            _charactersTransform.MovePlayerToNPC();
            _playerMotionController.СanAct = false;
            _gardenView.ShowEmptyGarden();

            ShowTutorialHand();
        }

        private void ShowTutorialHand()
        {
            _tutorialHand.SetActive(true);
            _tutorialHand.transform.DOScaleY(0.73f, 0.5f).SetEase(Ease.OutQuint).From(1f).SetLoops(-1, LoopType.Yoyo)
                .SetLink(_tutorialHand)
                .Play();
        }
    }
}