using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetsCollector : MonoBehaviour
    {
        private float _radius = 1f;
        private LayerMask _layerMask = ~0;
        private int _maxCapacity = 10;

        private SphereCollider _collider;
        private readonly List<Collider> _objects = new();

        private bool _isInited = false;

        public void Init(TargetSensorConfig sensorConfig)
        {
            _radius = sensorConfig.Radius;
            _layerMask = sensorConfig.LayerMask;
            _maxCapacity = sensorConfig.TargetsCapacity;

            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = _radius;

            _isInited = true;
        }

        private void OnValidate()
        {
            if (_collider == null) _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = _radius;
        }

        void Update()
        {
            for (var i = _objects.Count - 1; i >= 0; i--)
            {
                if (!_objects[i].enabled)
                    _objects.RemoveAt(i);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isInited) return;

            if (((1 << other.gameObject.layer) & _layerMask) == 0) return;

            if (_objects.Count >= _maxCapacity || _objects.Contains(other)) return;
            _objects.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInited) return;

            _objects.Remove(other);
        }

        public IReadOnlyList<Collider> GetRegisteredObjects()
        {
            return _objects.AsReadOnly();
        }

        public void Clear()
        {
            _objects.Clear();
        }

        public void ApplyConfig(TargetSensorConfig sensorConfig)
        {
            _radius = sensorConfig.Radius;
            _layerMask = sensorConfig.LayerMask;
            _maxCapacity = sensorConfig.TargetsCapacity;

            _collider.radius = _radius;
            if (_objects.Count > _maxCapacity)
                _objects.RemoveRange(_maxCapacity, _objects.Count - _maxCapacity);
        }
    }
}