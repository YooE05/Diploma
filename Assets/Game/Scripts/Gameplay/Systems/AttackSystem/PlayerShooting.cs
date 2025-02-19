using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerShooting : Listeners.IUpdateListener
    {
        private readonly TargetPicker _targetPicker;
        private readonly WeaponView[] _weaponsView;
        private readonly BulletsSystem _bulletsSystem;
        private readonly ShootingConfig _shootingConfig;

        private float _nextShotTime;

        public PlayerShooting(BulletsSystem bulletsSystem, WeaponView[] weaponsView, TargetPicker targetPicker,
            ShootingConfig shootingConfig)
        {
            _shootingConfig = shootingConfig;
            _targetPicker = targetPicker;
            _weaponsView = weaponsView;
            _bulletsSystem = bulletsSystem;
            _bulletsSystem.OnInit();
        }

        //TODO: change hiding weapon to rotate forward when stop shooting
        //TODO: remove change visibility from update
        public void OnUpdate(float deltaTime)
        {
            if (_targetPicker.TryGetNClosestTargetsPosition(_weaponsView.Length, out var targetsPositions))
            {
                for (var i = 0; i < _weaponsView.Length; i++)
                {
                    _weaponsView[i].RotateWeapon(targetsPositions[i]);
                }

                if ((Time.time < _nextShotTime)) return;
                _nextShotTime += _shootingConfig.ShootingDelay;

                for (var i = 0; i < _weaponsView.Length; i++)
                {
                    UpdateWeaponState(_weaponsView[i], targetsPositions[i]);
                }
            }
            else
            {
                _nextShotTime += Time.deltaTime;
                for (var i = 0; i < _weaponsView.Length; i++)
                {
                    _weaponsView[i].SetWeaponVisibility(false);
                }
            }
        }

        private void UpdateWeaponState(WeaponView weaponView, Vector3 targetPosition)
        {
            weaponView.SetWeaponVisibility(true);

            var velocity = CalculateBulletVelocity(
                GetShotDirection(weaponView.ShootingPointPosition, targetPosition), _shootingConfig.BulletSpeed);
            _bulletsSystem.FlyBullet(weaponView.ShootingPointPosition, _shootingConfig.Damage, velocity);
        }

        private Vector3 GetShotDirection(Vector3 startShootingPosition, Vector3 targetPosition)
        {
            var direction = targetPosition - startShootingPosition;
            return direction.normalized;
        }

        private Vector3 CalculateBulletVelocity(Vector3 direction, float speed)
        {
            var velocity = direction * speed;
            return velocity;
        }
    }
}