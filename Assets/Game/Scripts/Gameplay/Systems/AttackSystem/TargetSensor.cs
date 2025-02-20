using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TargetSensor
    {
        private LayerMask _layerMask;
        private int _targetsCapacity;
        private float _radius;

        public TargetSensor(TargetSensorConfig config)
        {
            ChangeConfig(config);
        }

        public void ChangeConfig(TargetSensorConfig config)
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