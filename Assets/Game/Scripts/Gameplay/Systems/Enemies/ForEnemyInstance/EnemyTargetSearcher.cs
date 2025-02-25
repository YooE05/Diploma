using System;
using UniRx;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyTargetSearcher
    {
        private readonly Transform _enemyTransform;
        private readonly TargetSensorConfig _waitPlayerSensorConfig;
        private readonly TargetSensorConfig _followPlayerSensorConfig;

        private readonly CompositeDisposable _disposables = new();
        private TargetSensor _findPlayerSensor;

        private readonly ReactiveProperty<bool> _playerIsFounded = new();
        public readonly ReactiveProperty<Collider> CurrentTarget = new();

        public EnemyTargetSearcher(Transform enemyTransform, TargetSensorConfig waitPlayerSensorConfig,
            TargetSensorConfig followPlayerSensorConfig)
        {
            _enemyTransform = enemyTransform;
            _waitPlayerSensorConfig = waitPlayerSensorConfig;
            _followPlayerSensorConfig = followPlayerSensorConfig;

            Init();
        }

        private void Init()
        {
            _findPlayerSensor = new TargetSensor(_waitPlayerSensorConfig);

            CurrentTarget.Value = null;
            _playerIsFounded.Value = false;
            _playerIsFounded.Subscribe(delegate { ChangeTargetSensorConfig(); })
                .AddTo(_disposables);
        }

        public void TryFindPlayer()
        {
            var targets = _findPlayerSensor.FindPossibleTargets(_enemyTransform.position, out var targetsCount);
            CurrentTarget.Value = targets[0];
            _playerIsFounded.Value = targetsCount > 0;
        }

        private void ChangeTargetSensorConfig()
        {
            _findPlayerSensor.ChangeConfig(CurrentTarget.Value is not null
                ? _followPlayerSensorConfig
                : _waitPlayerSensorConfig);
        }

        ~EnemyTargetSearcher()
        {
            _disposables.Dispose();
        }
    }
}