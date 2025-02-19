using System;
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

        private List<Collider> _currentGetTargets = new();

        public TargetPicker(Transform targetsOverlapCenter, TargetSensor targetsSensor)
        {
            _targetsOverlapCenter = targetsOverlapCenter;
            _targetsSensor = targetsSensor;
        }

        /*public bool TryGetClosestTargetPosition(out Vector3 closestTargetPosition)
        {
            if (TryGetClosestTarget(out Collider closestTarget))
            {
                closestTargetPosition = closestTarget.transform.position;
                return true;
            }

            closestTargetPosition = default;
            return false;
        }*/

        public bool TryGetNClosestTargetsPosition(int countOfWeapons, out Vector3[] closestTargetPosition)
        {
            closestTargetPosition = Array.Empty<Vector3>();
            _currentGetTargets.Clear();
            _currentGetTargets.AddRange(
                _targetsSensor.FindPossibleTargets(OverlapCenterPosition, out var targetsCount));
            _currentGetTargets.RemoveRange(targetsCount, _currentGetTargets.Count - targetsCount);
            if (targetsCount == 0) return false;

            //Array.Copy(_currentGetTargets, _currentGetTargets, targetsCount);
            //_currentGetTargets = _currentGetTargets.Take(targetsCount) as Collider[];
            closestTargetPosition = new Vector3[countOfWeapons];
            for (var i = 0; i < countOfWeapons; i++)
            {
                var closestTarget = GetClosestTarget(_currentGetTargets);
                if (_currentGetTargets.Count > 1)
                {
                    _currentGetTargets.Remove(closestTarget);
                }

                closestTargetPosition[i] = closestTarget.transform.position;
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

        /*private float GetSqrMagnitude(Transform targetTransform)
        {
            var heading = OverlapCenterPosition - targetTransform.position;
            return heading.sqrMagnitude;
        }*/
    }
}