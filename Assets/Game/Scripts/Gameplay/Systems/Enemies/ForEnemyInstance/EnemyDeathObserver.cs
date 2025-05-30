using System;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YooE.Diploma
{
    public sealed class EnemyDeathObserver : DeathObserver
    {
        public event Action OnDeathEnd;
        private readonly EnemyView _enemyView;
        private readonly AudioManager _audioManager;

        public EnemyDeathObserver(EnemyView enemyView, AudioManager audioManager)
        {
            _enemyView = enemyView;
            Init(_enemyView.HitPointsComponent);
            _audioManager = audioManager;
        }

        protected override void Init(HitPointsComponent hitPointsComponent)
        {
            _enemyView.AnimationEvents.OnDeathAnimationEnd += EndDeathActions;
            base.Init(hitPointsComponent);
        }

        protected override void StartDeathProcess(GameObject obj)
        {
            _enemyView.SetAnimatorTrigger("IsDead");
            _enemyView.DisablePhysics();
            base.StartDeathProcess(obj);

            if (_audioManager.TryGetAudioClipByName($"enemyDeath{Random.Range(1, 4)}", out var audioClip))
            {
                _audioManager.PlaySoundOneShot(audioClip, AudioOutput.Master, 0.8f);
            }
        }

        private void EndDeathActions()
        {
            _enemyView.AnimationEvents.OnDeathAnimationEnd -= EndDeathActions;
            _enemyView.DisableEnemy();
            OnDeathEnd?.Invoke();
            //Death particles
        }
    }
}