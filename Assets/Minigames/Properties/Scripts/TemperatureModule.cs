using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma.Properites
{
    public class TemperatureModule : MonoBehaviour, IInteractionModule
    {
        [SerializeField]
        private float _targetTemperature = 0f;

        private float _currentTemperature = 0f;

        public void Initialize(IReadOnlyList<ItemProperties> availableItems)
        {
            _currentTemperature = 0f;
            PropertiesUIManager.Instance.UpdateTemperatureDisplay(_currentTemperature);
        }

        public void OnItemClicked(ItemProperties item)
        {
            // No item selection; temperature controlled by slider
        }

        public void SetTemperature(float temperature)
        {
            _currentTemperature = temperature;
            PropertiesUIManager.Instance.UpdateTemperatureDisplay(_currentTemperature);
        }

        public bool CheckCondition()
        {
            return _currentTemperature >= _targetTemperature;
        }
    }
}