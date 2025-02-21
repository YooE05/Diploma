using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyDeathObserver : DeathObserver
    {
        public event Action OnDeathEnd;
        private readonly EnemyView _enemyView;

        public EnemyDeathObserver(EnemyView enemyView)
        {
            _enemyView = enemyView;
            Init(_enemyView.HitPointsComponent);
        }

        protected override void Init(HitPointsComponent hitPointsComponent)
        {
            _enemyView.AnimationEvents.OnDeathAnimationEnd += EndDeathActions;
            base.Init(hitPointsComponent);
        }

        protected override void StartDeathProcess(GameObject obj)
        {
            _enemyView.SetAnimatorTrigger("IsDead");
            base.StartDeathProcess(obj);
        }

        private void EndDeathActions()
        {
            _enemyView.AnimationEvents.OnDeathAnimationEnd -= EndDeathActions;
            _enemyView.DisableEnemy();
            OnDeathEnd?.Invoke();
            //Death particles
            //Add points to player
        }
    }
}