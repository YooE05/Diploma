using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TargetAttack
    {
        private readonly int _damage;
        private readonly AnimationEvents _animationEvents;
        private readonly TargetSensorConfig _targetSensorConfig;
        private readonly Transform _selfTransform;

        private TargetSensor _targetSensor;
        private HitPointsComponent _targetHp;

        public TargetAttack(int damage, AnimationEvents animationEvents, TargetSensorConfig targetSensorConfig,  Transform selfTransform)
        {
            _damage = damage;
            _animationEvents = animationEvents;
            _targetSensorConfig = targetSensorConfig;
            _selfTransform = selfTransform;

            Init();
        }

        private void Init()
        {
            _targetHp = null;
            _targetSensor = new TargetSensor(_targetSensorConfig);
            _animationEvents.OnHitEnded += DoDamage;
        }

        public void SetTargetHp(Collider target)
        {
            _targetHp = target?.GetComponent<HitPointsComponent>();
        }

        private void DoDamage()
        {
            if (CanDoDamage())
            {
                _targetHp.TakeDamage(_damage);
            }
        }

        private bool CanDoDamage()
        {
            _targetSensor.FindPossibleTargets(_selfTransform.position, out var targetCount);
            return targetCount > 0 && _targetHp;
        }
    }
}