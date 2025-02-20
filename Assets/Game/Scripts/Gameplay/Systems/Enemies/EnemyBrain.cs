using System;
using UniRx;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyBrain : MonoBehaviour //, Listeners.IInitListener, Listeners.IFinishListener
    {
        public event Action OnEnemyDied;
        [SerializeField] private EnemyAnimationEvents _animationEvents;

        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyRotation _rotation;

        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private TargetSensorConfig _waitPlayerSensorConfig;
        [SerializeField] private TargetSensorConfig _followPlayerSensorConfig;
        private TargetSensor _findPlayerSensor;

        private readonly ReactiveProperty<bool> _playerFounded = new();
        private Collider _playerCollider;
        private bool IsDead = false;

        private void Awake()
        {
            _findPlayerSensor = new TargetSensor(_waitPlayerSensorConfig);
            _hitPointsComponent.OnHpEmpty += StartDeathProcess;
            _animationEvents.OnDeathAnimationEnd += EnemyDeathActions;

            _playerCollider = null;
            _playerFounded.Value = false;
            _playerFounded.Subscribe(delegate { SetNewTarget(); })
                .AddTo(this);
        }

        private void SetNewTarget()
        {
            if (_playerCollider is not null)
            {
                _movement.SetTarget(_playerCollider.transform);
                _findPlayerSensor.ChangeConfig(_followPlayerSensorConfig);
            }
            else
            {
                _movement.SetTarget(null);
                _findPlayerSensor.ChangeConfig(_waitPlayerSensorConfig);
            }
        }

        private void Update()
        {
            if (IsDead) return;

            var targets = _findPlayerSensor.FindPossibleTargets(_enemyView.Position, out var targetsCount);
            _playerCollider = targets[0];
            _playerFounded.Value = targetsCount > 0;
            if (_playerFounded.Value == true)
            {
                _rotation.RotateToTarget(_playerCollider.transform);
            }

            _movement.Move();

            _enemyView.SetAnimatorBool("IsMoving", _movement.IsMoving);
            _enemyView.SetAnimatorBool("HasTarget", _movement.HasTarget);
        }

        private void StartDeathProcess(GameObject obj)
        {
            //DeathAnimation
            //disable movement
            _enemyView.SetAnimatorTrigger("IsDead");
            IsDead = true;
            _hitPointsComponent.OnHpEmpty -= StartDeathProcess;
        }

        private void EnemyDeathActions()
        {
            _animationEvents.OnDeathAnimationEnd -= EnemyDeathActions;
            _enemyView.DisableEnemy();
            //Death particles
            //Add points to player
            OnEnemyDied?.Invoke();
        }
    }
}