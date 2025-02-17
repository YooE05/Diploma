using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class PlayerShooting : MonoBehaviour, Listeners.IUpdateListener
    {
        [SerializeField] private TargetPicker _targetPicker;
        [SerializeField] private float _shootingDelay = 1.3f;
        private WeaponView _weaponView;
        private float _nextShotTime;

        private BulletsSystem _bulletsSystem;

        [SerializeField] private float _bulletSpeed;
        [SerializeField] private int _damage;

        [Inject]
        public void Construct(BulletsSystem bulletsSystem, PlayerView weaponView)
        {
            _weaponView = weaponView.WeaponView;
            _bulletsSystem = bulletsSystem;
            _bulletsSystem.OnInit();
        }

        //TODO: change hiding weapon to rotate forward when stop shooting
        //TODO: remove change visibility from update
        public void OnUpdate(float deltaTime)
        {
            if (_targetPicker.TryGetClosestTargetPosition(out var targetPosition))
            {
                _weaponView.RotateWeapon(targetPosition);

                if ((Time.time < _nextShotTime)) return;
                _nextShotTime += _shootingDelay;
                _weaponView.SetWeaponVisibility(true);

                var velocity = CalculateBulletVelocity(GetShotDirection(targetPosition), _bulletSpeed);
                _bulletsSystem.FlyBullet(_weaponView.ShootingPointPosition, _damage, velocity);
            }
            else
            {
                _nextShotTime += Time.deltaTime;
                _weaponView.SetWeaponVisibility(false);
            }
        }

        private Vector3 GetShotDirection(Vector3 targetPosition)
        {
            var direction = targetPosition - _weaponView.ShootingPointPosition;
            return direction.normalized;
        }

        private Vector3 CalculateBulletVelocity(Vector3 direction, float speed)
        {
            var velocity = direction * speed;
            return velocity;
        }
    }
}