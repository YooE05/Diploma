using System;
using Audio;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class PlayerDeathObserver : DeathObserver
    {
        public event Action OnDeathEnd;

        private readonly PlayerShooterBrain _playerShooterBrain;
        private readonly ShooterPopupsPresenter _popupsPresenter;
        private readonly bool _isEndless;

        [Inject] private AudioManager _audioManager;

        public PlayerDeathObserver(PlayerShooterBrain playerShooterBrain, ShooterPopupsPresenter popupsPresenter,
            bool isEndless = false)
        {
            _isEndless = isEndless;

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

            if (_audioManager.TryGetAudioClipByName($"playerDeath", out var audioClip))
            {
                _audioManager.PlaySoundOneShot(audioClip, AudioOutput.Master);
            }
        }

        private void EndDeathActions()
        {
            _playerShooterBrain.AnimationEvents.OnDeathAnimationEnd -= EndDeathActions;

            if (_isEndless)
            {
                _popupsPresenter.ShowEndGamePopup();
            }
            else
            {
                _popupsPresenter.ShowRetryPanel();
            }

            OnDeathEnd?.Invoke();
        }
    }
}