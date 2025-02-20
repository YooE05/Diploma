using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyRotation : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private float _rotationSpeed;


        private void Awake()
        {
            _agent.updateRotation = false;
        }

        public void RotateToTarget(Transform target)
        {
            var direction = target.position - _enemyTransform.position;
            _enemyTransform.rotation =
                Quaternion.Lerp(_enemyTransform.rotation, Quaternion.LookRotation(direction),
                    _rotationSpeed * Time.deltaTime);
        }
    }
}