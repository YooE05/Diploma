using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private Transform _target;

        public bool IsMoving = false;
        public bool HasTarget = false;

        public void SetTarget(Transform target)
        {
            _target = target;
            HasTarget = _target is not null;
        }

        public void Move()
        {
            if (_target is not null)
            {
                _agent.destination = _target.position;
            }

            IsMoving = true;
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance > _agent.stoppingDistance) return;
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    IsMoving = false;
                }
            }
        }
    }
}