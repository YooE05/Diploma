using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerDeathObserver : DeathObserver
    {
        public event Action OnDeathEnd;

        private readonly PlayerShooterBrain _playerShooterBrain;
        private readonly ShooterPopupsPresenter _popupsPresenter;

        public PlayerDeathObserver(PlayerShooterBrain playerShooterBrain, ShooterPopupsPresenter popupsPresenter)
        {
            _popupsPresenter = popupsPresenter;
            _playerShooterBrain = playerShooterBrain;
            Init(playerShooterBrain.GetHitPointsComponent());
        }

        protected override void Init(HitPointsComponent hitPointsComponent)
        {
            _playerShooterBrain.AnimationEvents.OnDeathAnimationEnd += EndDeathActions;
            base.Init(hitPointsComponent);
        }

        protected override void StartDeathProcess(GameObject obj)
        {
            _playerShooterBrain.StartDeathActions();
            base.StartDeathProcess(obj);
        }

        private void EndDeathActions()
        {
            _playerShooterBrain.AnimationEvents.OnDeathAnimationEnd -= EndDeathActions;
            _popupsPresenter.ShowRetryPanel();
            OnDeathEnd?.Invoke();
        }
    }
}