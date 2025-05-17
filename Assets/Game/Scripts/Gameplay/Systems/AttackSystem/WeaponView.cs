using System;
using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class WeaponView : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponRotationObject;
        private GameObject _currentWeaponView;

        [SerializeField] private Transform _shootingPoint;

        [SerializeField] private float _rotationSpeed;

        [SerializeField] private List<GameObject> _weaponView;

        private Vector3 _resultWeaponPosition;
        public Collider CurrentTarget { get; set; }
        public Vector3 ShootingPointPosition => _shootingPoint.position;
        private Transform WeaponViewTransform => _weaponRotationObject.transform;

        public void RotateWeapon()
        {
            if (!_currentWeaponView.activeSelf) return;

            _resultWeaponPosition =
                new Vector3(CurrentTarget.transform.position.x, 0f, CurrentTarget.transform.position.z);
            var direction = _resultWeaponPosition - WeaponViewTransform.position;

            MotionUseCases.Rotate(WeaponViewTransform, direction, _rotationSpeed * Time.deltaTime);
        }

        public void SetWeaponVisibility(bool isActive, EnemyType shootingConfigEnemyType)
        {
            for (var i = 0; i < _weaponView.Count; i++)
            {
                _weaponView[i].SetActive(false);
            }

            _currentWeaponView = shootingConfigEnemyType switch
            {
                EnemyType.Cactus => _weaponView[2],
                EnemyType.Mushroom => _weaponView[1],
                EnemyType.Any => _weaponView[0],
                _ => throw new ArgumentOutOfRangeException(nameof(shootingConfigEnemyType), shootingConfigEnemyType,
                    null)
            };

            _currentWeaponView.SetActive(isActive);
        }
    }
}