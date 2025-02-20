using UnityEngine;

namespace YooE.Diploma
{
    public sealed class WeaponView : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponView;
        [SerializeField] private Transform _shootingPoint;

        [SerializeField] private float _rotateSpeed;

        private Vector3 _resultWeaponPosition;
        public Collider CurrentTarget { get; set; }

        public Vector3 ShootingPointPosition => _shootingPoint.position;
        private Transform WeaponViewTransform => _weaponView.transform;

        public void RotateWeapon()
        {
            _resultWeaponPosition =
                new Vector3(CurrentTarget.transform.position.x, 0f, CurrentTarget.transform.position.z);

            var direction = _resultWeaponPosition - WeaponViewTransform.position;
            WeaponViewTransform.rotation = Quaternion.Lerp(WeaponViewTransform.rotation,
                Quaternion.LookRotation(direction), Time.deltaTime * _rotateSpeed);

            /* WeaponViewTransform.LookAt(_resultWeaponPosition);*/
        }

        public void SetWeaponVisibility(bool isActive)
        {
            _weaponView.SetActive(isActive);
        }
    }
}