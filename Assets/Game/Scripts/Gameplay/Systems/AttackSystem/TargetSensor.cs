using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TargetSensor
    {
        private readonly LayerMask _layerMask;
        private readonly int _targetsCapacity;
        private readonly float _radius;
        private readonly Transform _overlapCenter;

        public TargetSensor(TargetSensorConfig config, Transform overlapCenter)
        {
            _layerMask = config.LayerMask;
            _targetsCapacity = config.TargetsCapacity;
            _radius = config.Radius;
            _overlapCenter = overlapCenter;
        }

        public Collider[] FindPossibleTargets(out int detectedColliderCount)
        {
            detectedColliderCount = 0;
            var results = new Collider[_targetsCapacity];
            detectedColliderCount =
                Physics.OverlapSphereNonAlloc(_overlapCenter.position, _radius, results, _layerMask);
            return results;
        }
    }
}