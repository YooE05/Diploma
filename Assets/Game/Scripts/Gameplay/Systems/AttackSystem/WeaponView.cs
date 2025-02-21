using UnityEngine;

namespace YooE.Diploma
{
    public sealed class WeaponView : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponView;
        [SerializeField] private Transform _shootingPoint;

        [SerializeField] private float _rotationSpeed;

        private Vector3 _resultWeaponPosition;
        public Collider CurrentTarget { get; set; }
        public Vector3 ShootingPointPosition => _shootingPoint.position;
        private Transform WeaponViewTransform => _weaponView.transform;

        public void RotateWeapon()
        {
            _resultWeaponPosition =
                new Vector3(CurrentTarget.transform.position.x, 0f, CurrentTarget.transform.position.z);
            var direction = _resultWeaponPosition - WeaponViewTransform.position;

            MotionUseCases.Rotate(WeaponViewTransform, direction, _rotationSpeed * Time.deltaTime);
        }

        public void SetWeaponVisibility(bool isActive)
        {
            _weaponView.SetActive(isActive);
        }
    }
}