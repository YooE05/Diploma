using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace YooE.Diploma
{
    public sealed class TargetPicker
    {
        private readonly TargetSensor _targetsSensor;
        private readonly Transform _targetsOverlapCenter;
        private Vector3 OverlapCenterPosition => _targetsOverlapCenter.position;

        private readonly List<Collider> _currentGetTargets = new();

        public TargetPicker(Transform targetsOverlapCenter, TargetSensor targetsSensor)
        {
            _targetsOverlapCenter = targetsOverlapCenter;
            _targetsSensor = targetsSensor;
        }

        public bool TryGetNClosestTargets(int countOfWeapons, out List<Collider> closestTargetPosition)
        {
            closestTargetPosition = new List<Collider>();
            _currentGetTargets.Clear();
            _currentGetTargets.AddRange(
                _targetsSensor.FindPossibleTargets(OverlapCenterPosition, out var targetsCount));
            _currentGetTargets.RemoveRange(targetsCount, _currentGetTargets.Count - targetsCount);
            if (targetsCount == 0) return false;

            var diff = countOfWeapons - targetsCount;

            for (var i = 0; i < countOfWeapons; i++)
            {
                var closestTarget = GetClosestTarget(_currentGetTargets);
                if (diff - i <= 0)
                {
                    _currentGetTargets.Remove(closestTarget);
                }

                closestTargetPosition.Add(closestTarget);
            }

            return true;
        }

        private Collider GetClosestTarget(List<Collider> targets)
        {
            return targets?.Where(t => t is not null).OrderBy(GetSqrMagnitude).First();
        }

        private float GetSqrMagnitude(Collider target)
        {
            var heading = OverlapCenterPosition - target.transform.position;
            return heading.sqrMagnitude;
        }
    }
}