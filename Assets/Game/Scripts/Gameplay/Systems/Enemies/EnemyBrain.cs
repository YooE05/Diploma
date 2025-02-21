using UniRx;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private EnemyView _enemyView;

        private EnemyTargetSearcher _targetSearcher;
        private EnemyMotionController _motionController;
        private EnemyDeathObserver _deathObserver;
        private TargetAttack _targetAttack;

        private EnemyConfig _enemyConfig;

        private bool _canAct;

        [Inject]
        public void Construct(EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
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

            _targetSearcher.CurrentTarget.Subscribe(_targetAttack.SetTargetHp).AddTo(this);
            _canAct = true;

            _deathObserver.OnDeathStart += delegate { _canAct = false; };
            _deathObserver.OnDeathEnd += OnDeathEndActions;
        }

        private void OnDeathEndActions()
        {
            Debug.Log("+10 points!");
            _deathObserver.OnDeathEnd -= OnDeathEndActions;
        }

        private void Update()
        {
            if (!_canAct) return;

            _motionController.MoveAndRotate();
            _targetSearcher.TryFindPlayer();
            //_targetAttack.SetTargetHp(_targetSearcher.CurrentTarget.Value);
        }
    }
}