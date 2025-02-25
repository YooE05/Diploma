using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyMovement
    {
        private readonly NavMeshAgent _agent;
        private Transform _targetTransform;

        public bool IsMoving;
        public bool HasTarget;

        public EnemyMovement(NavMeshAgent agent, float speed)
        {
            _agent = agent;
            _agent.speed = speed;
            IsMoving = false;
            HasTarget = false;
        }

        /*public void SetTarget(Transform target)
        {
            _target = target;
            HasTarget = _target is not null;
        }*/

        public void SetTarget(Collider targetCollider)
        {
            _targetTransform = targetCollider ? targetCollider.transform : null;
            HasTarget = _targetTransform is not null;
        }

        public void Move()
        {
            if (HasTarget)
            {
                _agent.destination = _targetTransform.position;
            }

            IsMoving = true;
            if (_agent.pathPending) return;

            if (_agent.remainingDistance > _agent.stoppingDistance) return;
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                IsMoving = false;
            }
        }
    }
}