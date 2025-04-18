using UniRx;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyTargetSearcher
    {
        private readonly TargetsCollector _targetsCollector;
        private readonly TargetSensorConfig _waitPlayerSensorConfig;
        private readonly TargetSensorConfig _followPlayerSensorConfig;

        private readonly CompositeDisposable _disposables = new();

        private readonly ReactiveProperty<bool> _playerIsFounded = new();
        public readonly ReactiveProperty<Collider> CurrentTarget = new();

        public EnemyTargetSearcher(TargetsCollector targetsCollector,
            TargetSensorConfig waitPlayerSensorConfig,
            TargetSensorConfig followPlayerSensorConfig)
        {
            _targetsCollector = targetsCollector;
            _waitPlayerSensorConfig = waitPlayerSensorConfig;
            _followPlayerSensorConfig = followPlayerSensorConfig;

            Init();
        }

        private void Init()
        {
            _targetsCollector.Init(_waitPlayerSensorConfig);

            CurrentTarget.Value = null;
            _playerIsFounded.Value = false;
            _playerIsFounded.Subscribe(delegate { ChangeTargetSensorConfig(); })
                .AddTo(_disposables);
        }

        public void TryFindPlayer()
        {
            var targets = _targetsCollector.GetRegisteredObjects();
            CurrentTarget.Value = targets.Count > 0 ? targets[0] : null;
            _playerIsFounded.Value = targets.Count > 0;
        }

        private void ChangeTargetSensorConfig()
        {
            _targetsCollector.ApplyConfig(CurrentTarget.Value is not null
                ? _followPlayerSensorConfig
                : _waitPlayerSensorConfig);
        }

        ~EnemyTargetSearcher()
        {
            _disposables.Dispose();
        }
    }
}