using System;
using UniRx;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyTargetSearcher
    {
        // private readonly Transform _enemyTransform;
        private readonly TargetsCollector _targetsCollector;
        private readonly TargetSensorConfig _waitPlayerSensorConfig;
        private readonly TargetSensorConfig _followPlayerSensorConfig;

        private readonly CompositeDisposable _disposables = new();
        // private TargetSensor _findPlayerSensor;

        private readonly ReactiveProperty<bool> _playerIsFounded = new();
        public readonly ReactiveProperty<Collider> CurrentTarget = new();

        public EnemyTargetSearcher(Transform enemyTransform, TargetsCollector targetsCollector,
            TargetSensorConfig waitPlayerSensorConfig,
            TargetSensorConfig followPlayerSensorConfig)
        {
            // _enemyTransform = enemyTransform;
            _targetsCollector = targetsCollector;
            _waitPlayerSensorConfig = waitPlayerSensorConfig;
            _followPlayerSensorConfig = followPlayerSensorConfig;

            Init();
        }

        private void Init()
        {
            // _findPlayerSensor = new TargetSensor(_waitPlayerSensorConfig);
            _targetsCollector.Init(_waitPlayerSensorConfig);

            CurrentTarget.Value = null;
            _playerIsFounded.Value = false;
            _playerIsFounded.Subscribe(delegate { ChangeTargetSensorConfig(); })
                .AddTo(_disposables);
        }

        public void TryFindPlayer()
        {
            var targets =
                _targetsCollector
                    .GetRegisteredObjects(); //_findPlayerSensor.FindPossibleTargets(_enemyTransform.position, out var targetsCount);
            CurrentTarget.Value = targets.Count > 0 ? targets[0] : null;
            _playerIsFounded.Value = targets.Count > 0;
        }

        private void ChangeTargetSensorConfig()
        {
            _targetsCollector.ApplyConfig(CurrentTarget.Value is not null
                ? _followPlayerSensorConfig
                : _waitPlayerSensorConfig);

            /*_findPlayerSensor.ChangeConfig(CurrentTarget.Value is not null
                ? _followPlayerSensorConfig
                : _waitPlayerSensorConfig);*/
        }

        ~EnemyTargetSearcher()
        {
            _disposables.Dispose();
        }
    }
}