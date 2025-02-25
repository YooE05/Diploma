using UnityEngine;
using UnityEngine.AI;

namespace YooE.Diploma
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyGO;
        [SerializeField] private Animator _animator;

        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public HitPointsComponent HitPointsComponent { get; private set; }
        [field: SerializeField] public AnimationEvents AnimationEvents { get; private set; }

        public Transform Transform => _enemyGO.transform;

        public void DisableEnemy()
        {
            _enemyGO.SetActive(false);
        }

        public void SetAnimatorBool(string varName, bool conditions)
        {
            _animator.SetBool(varName, conditions);
        }

        public void SetAnimatorTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }
    }
}