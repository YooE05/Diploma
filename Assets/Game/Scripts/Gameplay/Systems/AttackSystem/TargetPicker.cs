using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TargetPicker : MonoBehaviour
    {
        [SerializeField] private Transform _targetsOverlapCenter;
        [SerializeField] private TargetSensorConfig _targetSensorConfig;

        private TargetSensor _targetsSensor;

        private void Awake()
        {
            _targetsSensor = new TargetSensor(_targetSensorConfig, _targetsOverlapCenter);
        }

        public bool TryGetClosestTargetPosition(out Vector3 closestTargetPosition)
        {
            if (TryGetClosestTarget(out Collider closestTarget))
            {
                closestTargetPosition = closestTarget.transform.position;
                return true;
            }

            closestTargetPosition = default;
            return false;
        }

        public bool TryGetClosestTarget(out Collider closestTarget)
        {
            var targets = _targetsSensor.FindPossibleTargets(out var targetsCount);
            closestTarget = null;
            if (targets[0] == null) return false;

            closestTarget = targets[0];
            var minSqrDistance = GetSqrMagnitude(closestTarget.transform);
            for (var i = 1; i < targets.Length; i++)
            {
                if (targets[i] == null) break;

                var newSqrMagnitude = GetSqrMagnitude(targets[i].transform);

                if (newSqrMagnitude <= minSqrDistance)
                {
                    minSqrDistance = newSqrMagnitude;
                    closestTarget = targets[i];
                }
            }

            return true;
        }

        private float GetSqrMagnitude(Transform targetTransform)
        {
            var heading = _targetsOverlapCenter.position - targetTransform.position;
            return heading.sqrMagnitude;
        }
    }
}