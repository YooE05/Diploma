using UnityEngine;

namespace YooE.Diploma
{
    public sealed class WeaponView : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponView;
        [SerializeField] private Transform _shootingPoint;

        public Vector3 ShootingPointPosition => _shootingPoint.position;
        private Transform WeaponViewTransform => _weaponView.transform;

        public void RotateWeapon(Vector3 targetPosition)
        {
            var resultPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
            WeaponViewTransform.LookAt(resultPosition);
        }

        public void SetWeaponVisibility(bool isActive)
        {
            _weaponView.SetActive(isActive);
        }
    }
}