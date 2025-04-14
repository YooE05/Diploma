using System;
using UniRx;

namespace YooE.Diploma
{
    public sealed class EnemyBrain
    {
        public event Action<EnemyBrain> OnDied;

        private readonly CompositeDisposable _disposable = new();
        private readonly EnemyConfig _enemyConfig;

        private EnemyTargetSearcher _targetSearcher;
        private EnemyMotionController _motionController;
        private EnemyDeathObserver _deathObserver;
        private TargetAttack _targetAttack;

        public EnemyView View { get; }

        private bool _canAct;

        public EnemyBrain(EnemyConfig enemyConfig, EnemyView enemyView)
        {
            _enemyConfig = enemyConfig;
            View = enemyView;
            Init();
        }

        private void Init()
        {
            View.HitPointsComponent.SetStartHp(_enemyConfig.HitPoints);
            _targetSearcher =
                new EnemyTargetSearcher(View.Transform, _enemyConfig.WaitPlayerSensorConfig,
                    _enemyConfig.FollowPlayerSensorConfig);
            _motionController =
                new EnemyMotionController(View, _enemyConfig.RotationSpeed, _enemyConfig.MovementSpeed,
                    _targetSearcher);
            _deathObserver = new EnemyDeathObserver(View);
            _targetAttack = new TargetAttack(_enemyConfig.Damage, View.AnimationEvents,
                _enemyConfig.AttackRangeSensorConfig,
                View.Transform);

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
            OnDied?.Invoke(this);
            _deathObserver.OnDeathEnd -= OnDeathEndActions;
        }

        ~EnemyBrain()
        {
            _disposable.Dispose();
        }
    }
}