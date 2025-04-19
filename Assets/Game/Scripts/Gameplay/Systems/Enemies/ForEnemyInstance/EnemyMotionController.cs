using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyMotionController
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly EnemyTargetSearcher _enemyTargetSearcher;
        private readonly NavMeshAgent _agent;
        private readonly EnemyView _enemyView;
        private readonly float _rotationSpeed;
        private readonly float _movementSpeed;

        private EnemyMovement _movement;
        private EnemyRotation _rotation;

        private bool _needRotate;

        public EnemyMotionController(EnemyView enemyView, float rotationSpeed, float movementSpeed,
            EnemyTargetSearcher enemyTargetSearcher)
        {
            _agent = enemyView.Agent;
            _enemyView = enemyView;
            _rotationSpeed = rotationSpeed;
            _movementSpeed = movementSpeed;
            _enemyTargetSearcher = enemyTargetSearcher;

            Init();
        }

        private void Init()
        {
            _movement = new EnemyMovement(_agent, _movementSpeed);
            _rotation = new EnemyRotation(_agent, _enemyView.Transform, _rotationSpeed);

            _needRotate = false;
            _enemyTargetSearcher.CurrentTarget.Subscribe(delegate
            {
                SetNewTarget(_enemyTargetSearcher.CurrentTarget.Value);
            }).AddTo(_disposables);

            //  _enemyTargetSearcher.OnTargetChanged += SetNewTarget;
        }

        public void StopMotion()
        {
            _enemyView.SetAnimatorTrigger("IsFinish");
        }

        public void MoveAndRotate()
        {
            if (_needRotate)
            {
                _rotation.RotateToTarget(_enemyTargetSearcher.CurrentTarget.Value.transform);
            }

            _movement.Move();

            _enemyView.SetAnimatorBool("IsMoving", _movement.IsMoving);
            _enemyView.SetAnimatorBool("HasTarget", _movement.HasTarget);
        }

        private void SetNewTarget(Collider target)
        {
            if (target is null)
            {
                _movement.SetTarget(null);
            }
            else
            {
                _movement.SetTarget(target);
            }

            _needRotate = _enemyTargetSearcher.CurrentTarget.Value is not null;
        }

        ~EnemyMotionController()
        {
            //  _enemyTargetSearcher.OnTargetChanged -= SetNewTarget;
            _disposables.Dispose();
        }
    }
}