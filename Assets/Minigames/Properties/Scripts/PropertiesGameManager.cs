using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma.Properites
{
    public class PropertiesGameManager : MonoBehaviour
    {
        [SerializeField] private List<ItemProperties> _allItems = new();

        private IInteractionModule _currentModule;
        private int _moduleIndex = 0;
        private readonly List<IInteractionModule> _modules = new();

        private void Start()
        {
            _modules.Add(GetComponent<MassModule>());
            _modules.Add(GetComponent<VolumeModule>());
            _modules.Add(GetComponent<BuoyancyModule>());
            _modules.Add(GetComponent<TemperatureModule>());

            GoToNextModule();
        }

        public void GoToNextModule()
        {
            if (_moduleIndex >= _modules.Count)
            {
                PropertiesUIManager.Instance.ShowCompletion();
                return;
            }

            _currentModule = _modules[_moduleIndex];
            _currentModule.Initialize(_allItems);
            _moduleIndex++;
        }

        public void OnItemClicked(ItemProperties item)
        {
            _currentModule.OnItemClicked(item);
        }

        public void OnCheckIsComplete()
        {
            if (_currentModule.CheckCondition())
            {
                PropertiesUIManager.Instance.ShowSuccess();
            }
        }

        public void OnTemperatureChanged(float temperature)
        {
            if (_currentModule is TemperatureModule temperatureModule)
            {
                temperatureModule.SetTemperature(temperature);

                if (temperatureModule.CheckCondition())
                {
                    PropertiesUIManager.Instance.ShowSuccess();
                    Invoke(nameof(GoToNextModule), 2f);
                }
            }
        }
    }
}