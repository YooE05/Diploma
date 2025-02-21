using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyGO;
        [SerializeField] private Animator _animator;

        public Vector3 Position => _enemyGO.transform.position;
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