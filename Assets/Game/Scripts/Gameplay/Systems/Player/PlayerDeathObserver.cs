using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerDeathObserver : DeathObserver
    {
        public event Action OnDeathEnd;

        private readonly PlayerView _playerView;

        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerShooting _playerShooting;
        private readonly PlayerAnimation _playerAnimation;

        public PlayerDeathObserver(PlayerView playerView,
            PlayerMotionController playerMotionController, PlayerShooting playerShooting,
            PlayerAnimation playerAnimation)
        {
            _playerView = playerView;
            _playerMotionController = playerMotionController;
            _playerShooting = playerShooting;
            _playerAnimation = playerAnimation;
            Init(playerView.HitPointsComponent);
        }

        protected override void Init(HitPointsComponent hitPointsComponent)
        {
            _playerAnimation.AnimationEvents.OnDeathAnimationEnd += EndDeathActions;
            base.Init(hitPointsComponent);
        }

        protected override void StartDeathProcess(GameObject obj)
        {
            _playerMotionController.CanAct = false;
            _playerAnimation.SetAnimatorTrigger("IsDead");

            //Remove target from Enemy
            _playerView.DisablePlayerCharacterController();

            //Stop AttackSystem
            _playerShooting.DisableShooting();

            base.StartDeathProcess(obj);
        }

        private void EndDeathActions()
        {
            _playerAnimation.AnimationEvents.OnDeathAnimationEnd -= EndDeathActions;
            //_playerView.DisablePlayerView();
            OnDeathEnd?.Invoke();
            //ShowDeathPopUp or Restart Level
        }
    }
}