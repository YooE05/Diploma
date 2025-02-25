using System;
using UniRx;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyBrain
    {
        public event Action<EnemyBrain> OnDead;

        private readonly CompositeDisposable _disposable = new();
        private readonly EnemyView _enemyView;
        private readonly EnemyConfig _enemyConfig;

        private EnemyTargetSearcher _targetSearcher;
        private EnemyMotionController _motionController;
        private EnemyDeathObserver _deathObserver;
        private TargetAttack _targetAttack;

        private bool _canAct;

        public EnemyBrain(EnemyConfig enemyConfig, EnemyView enemyView)
        {
            _enemyConfig = enemyConfig;
            _enemyView = enemyView;
            Init();
        }

        private void Init()
        {
            _targetSearcher =
                new EnemyTargetSearcher(_enemyView.Transform, _enemyConfig.WaitPlayerSensorConfig,
                    _enemyConfig.FollowPlayerSensorConfig);
            _motionController =
                new EnemyMotionController(_enemyView, _enemyConfig.RotationSpeed, _enemyConfig.MovementSpeed,
                    _targetSearcher);
            _deathObserver = new EnemyDeathObserver(_enemyView);
            _targetAttack = new TargetAttack(_enemyConfig.Damage, _enemyView.AnimationEvents,
                _enemyConfig.AttackRangeSensorConfig,
                _enemyView.Transform);

            _targetSearcher.CurrentTarget.Subscribe(_targetAttack.SetTargetHp).AddTo(_disposable);
            _canAct = true;

            _deathObserver.OnDeathStart += DeathStartActions;
            _deathObserver.OnDeathEnd += OnDeathEndActions;
        }

        public void Update()
        {
            if (!_canAct) return;

            _motionController.MoveAndRotate();
            _targetSearcher.TryFindPlayer();
        }

        private void DeathStartActions()
        {
            _canAct = false;
            _targetAttack.DisableAttackAbility();
            _deathObserver.OnDeathStart -= DeathStartActions;
        }

        private void OnDeathEndActions()
        {
            OnDead?.Invoke(this);
            _deathObserver.OnDeathEnd -= OnDeathEndActions;
        }

        ~EnemyBrain()
        {
            _disposable.Dispose();
        }
    }
}