using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TargetAttack : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _targetHP;
        [SerializeField] private int _damage;
        [SerializeField] private EnemyAnimationEvents _animationEvents;
        [SerializeField] private EnemyMovement _movement;

        [SerializeField] private TargetSensorConfig _targetSensorConfig;
        private TargetSensor _targetSensor;

        private void Awake()
        {
            _targetSensor = new TargetSensor(_targetSensorConfig);
            _animationEvents.OnPunchEnded += DoDamage;
        }

        private void DoDamage()
        {
            if (CanDoDamage())
            {
                _targetHP.TakeDamage(_damage);
            }
        }

        private bool CanDoDamage()
        {
            _targetSensor.FindPossibleTargets(transform.position, out var targetCount);
            return targetCount > 0;
        }
    }
}