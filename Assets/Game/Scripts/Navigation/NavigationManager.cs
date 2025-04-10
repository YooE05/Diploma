using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    public sealed class NavigationManager : MonoBehaviour
    {
        [SerializeField] private NavigationArrow _arrow;

        [SerializeField] private Transform _playerTransform;

        [PropertySpace]
        [ReadOnly]
        [ShowInInspector]
        private Vector3 _targetPosition;

        [ReadOnly] [ShowInInspector] private bool _isActive;

        private void Awake()
        {
            _arrow.Hide();
        }

        private void Update()
        {
            if (_isActive)
            {
                _arrow.SetPosition(_playerTransform.position);
                _arrow.LookAt(_targetPosition);
            }
        }

        [Button]
        public void StartLookAt(Transform targetPoint)
        {
            StartLookAt(targetPoint.position);
        }

        public void StartLookAt(Vector3 targetPosition)
        {
            _arrow.Show();
            _isActive = true;
            _targetPosition = targetPosition;
        }

        [Button]
        public void Stop()
        {
            _arrow.Hide();
            _isActive = false;
        }
    }
}