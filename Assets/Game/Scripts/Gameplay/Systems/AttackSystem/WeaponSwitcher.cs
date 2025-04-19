using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class WeaponSwitcher : MonoBehaviour
    {
        private PlayerShooting _playerShooting;

        [SerializeField] private List<ButtonView> _weaponButtons;
        [SerializeField] private List<ShootingConfig> _shootingConfigs;

        [Inject]
        public void Construct(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
            ClearOtherSwitches(0);
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (_weaponButtons.Count != _shootingConfigs.Count) return;

            for (var i = 0; i < _weaponButtons.Count; i++)
            {
                _weaponButtons[i].OnCurrentButtonClicked += EnableWeapon;
            }
        }

        private void Unsubscribe()
        {
            if (_weaponButtons.Count != _shootingConfigs.Count) return;

            for (var i = 0; i < _weaponButtons.Count; i++)
            {
                _weaponButtons[i].OnCurrentButtonClicked -= EnableWeapon;
            }
        }

        private void EnableWeapon(ButtonView buttonView)
        {
            var index = _weaponButtons.IndexOf(buttonView);
            ClearOtherSwitches(index);
            SwitchWeapon(_shootingConfigs[index]);
        }

        private void ClearOtherSwitches(int weaponIndex)
        {
            for (var i = 0; i < _weaponButtons.Count; i++)
            {
                if (i == weaponIndex)
                {
                    ((SwitchButtonView)_weaponButtons[i]).SetSwitchPosition(true);
                    continue;
                }

                ((SwitchButtonView)_weaponButtons[i]).SetSwitchPosition(false);
            }
        }

        public void SwitchWeapon(ShootingConfig shootingConfig)
        {
            _playerShooting.ChangeShootingConfig(shootingConfig);
        }
    }
}