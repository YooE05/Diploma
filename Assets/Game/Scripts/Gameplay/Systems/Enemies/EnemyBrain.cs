using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyBrain : MonoBehaviour
    {
        private EnemyTargetSearcher _targetSearcher;
        private EnemyMotionController _motionController;
        private DeathObserver _deathObserver;
        private TargetAttack _targetAttack;

        [SerializeField] private EnemyView _enemyView;

        [SerializeField] private TargetSensorConfig _waitPlayerSensorConfig;
        [SerializeField] private TargetSensorConfig _followPlayerSensorConfig;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _movementSpeed;

        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private EnemyAnimationEvents _animationEvents;

        [SerializeField] private int _damage;
        [SerializeField] private TargetSensorConfig _attackRangeSensorConfig;

        private bool _canAct;

        private void Awake()
        {
            _targetSearcher =
                new EnemyTargetSearcher(_enemyView.Transform, _waitPlayerSensorConfig, _followPlayerSensorConfig);
            _motionController =
                new EnemyMotionController(_agent, _enemyView, _rotationSpeed, _movementSpeed, _targetSearcher);
            _deathObserver = new DeathObserver(_hitPointsComponent, _enemyView, _animationEvents);
            _targetAttack = new TargetAttack(_damage, _animationEvents, _attackRangeSensorConfig, _enemyView.Transform);

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