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
                if (Vector3.SqrMagnitude(_playerTransform.position - _targetPosition) < 7)
                {
                    _arrow.Hide();
                    return;
                }

                _arrow.Show();
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

        [Header("Targets")] [SerializeField] private Transform _mainNPC;
        [SerializeField] private Transform _door;
        [SerializeField] private Transform _seed;
        [SerializeField] private Transform _garden;
        [SerializeField] private Transform _lever;
        [SerializeField] private Transform _shop;

        public void SetNavigationToMainNpc()
        {
            StartLookAt(_mainNPC);
        }

        public void SetNavigationToDoor()
        {
            StartLookAt(_door);
        }

        public void SetNavigationToSeed()
        {
            StartLookAt(_seed);
        }

        public void SetNavigationToGarden()
        {
            StartLookAt(_garden);
        }

        public void SetNavigationToLever()
        {
            StartLookAt(_lever);
        }
        
        public void SetNavigationToShop()
        {
            StartLookAt(_shop);
        }
    }
}