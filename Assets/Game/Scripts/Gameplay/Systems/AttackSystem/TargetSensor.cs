using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TargetSensor
    {
        private readonly LayerMask _layerMask;
        private readonly int _targetsCapacity;
        private readonly float _radius;

        public TargetSensor(TargetSensorConfig config)
        {
            _layerMask = config.LayerMask;
            _targetsCapacity = config.TargetsCapacity;
            _radius = config.Radius;
        }

        public Collider[] FindPossibleTargets(Vector3 overlapCenterPosition, out int detectedColliderCount)
        {
            detectedColliderCount = 0;
            var results = new Collider[_targetsCapacity];
            detectedColliderCount =
                Physics.OverlapSphereNonAlloc(overlapCenterPosition, _radius, results, _layerMask);
            return results;
        }
    }
}