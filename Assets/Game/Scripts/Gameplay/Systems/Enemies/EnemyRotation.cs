using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyRotation
    {
        private readonly Transform _enemyTransform;
        private readonly float _rotationSpeed;

        public EnemyRotation(NavMeshAgent agent, Transform enemyTransform, float rotationSpeed)
        {
            _enemyTransform = enemyTransform;
            _rotationSpeed = rotationSpeed;

            agent.updateRotation = false;
        }

        public void RotateToTarget(Transform target)
        {
            var direction = target.position - _enemyTransform.position;
            MotionUseCases.Rotate(_enemyTransform, direction, _rotationSpeed * Time.deltaTime);
        }
    }
}