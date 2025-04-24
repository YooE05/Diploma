using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YooE.Diploma.Properites
{
    public class PropertiesUIManager : MonoBehaviour
    {
        public static PropertiesUIManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _massText;
        [SerializeField] private TextMeshProUGUI _volumeText;
        [SerializeField] private Slider _temperatureSlider;

        [SerializeField] private GameObject _goNextModulePanel;
        [SerializeField] private GameObject _checkButton;

        [SerializeField] private List<SwitchButtonView> _massModuleItems;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void DisableAllInteractionExceptGoNext()
        {
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].DisableButton();
            }
        }

        private void HideAllModuleUI()
        {
            _goNextModulePanel.SetActive(false);

            HideMassModuleUI();
            HideVolumeModuleUI();
            HideBuoyancyModuleUI();
            _temperatureSlider.gameObject.SetActive(false);
        }

        public void ShowMassModuleUI()
        {
            _checkButton.SetActive(true);
            HideAllModuleUI();
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].EnableButton();
                _massModuleItems[i].SetSwitchPosition(false);
                _massModuleItems[i].OnButtonClicked += _massModuleItems[i].Switch;
            }

            _massText.gameObject.SetActive(true);
        }

        private void HideMassModuleUI()
        {
            _checkButton.SetActive(false);
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].DisableButton();
                _massModuleItems[i].SetSwitchPosition(false);
                _massModuleItems[i].OnButtonClicked -= _massModuleItems[i].Switch;
            }

            _massText.gameObject.SetActive(false);
        }

        public void ShowVolumeModuleUI()
        {
            _checkButton.SetActive(true);
            HideAllModuleUI();
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].EnableButton();
                _massModuleItems[i].SetSwitchPosition(false);
                _massModuleItems[i].OnButtonClicked += _massModuleItems[i].Switch;
            }

            _volumeText.gameObject.SetActive(true);
        }

        private void HideVolumeModuleUI()
        {
            _checkButton.SetActive(false);
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].DisableButton();
                _massModuleItems[i].SetSwitchPosition(false);
                _massModuleItems[i].OnButtonClicked -= _massModuleItems[i].Switch;
            }

            _volumeText.gameObject.SetActive(false);
        }

        public void ShowBuoyancyModuleUI()
        {
            _checkButton.SetActive(true);
            HideAllModuleUI();
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].EnableButton();
                _massModuleItems[i].SetSwitchPosition(false);
                _massModuleItems[i].OnButtonClicked += _massModuleItems[i].Switch;
            }
        }

        private void HideBuoyancyModuleUI()
        {
            _checkButton.SetActive(true);
            for (var i = 0; i < _massModuleItems.Count; i++)
            {
                _massModuleItems[i].DisableButton();
                _massModuleItems[i].SetSwitchPosition(false);
                _massModuleItems[i].OnButtonClicked -= _massModuleItems[i].Switch;
            }
        }

        public void ShowTemperatureModuleUI()
        {
            _checkButton.SetActive(true);
            HideAllModuleUI();
            _temperatureSlider.gameObject.SetActive(true);
        }

        public void UpdateMassDisplay(float current, float target)
        {
            _massText.text = $"Mass: {current:F1} / {target:F1} kg";
        }

        public void UpdateVolumeDisplay(float current, float target)
        {
            _volumeText.text = $"Volume: {current:F1} / {target:F1} L";
        }

        public void UpdateTemperatureDisplay(float temperature)
        {
            _temperatureSlider.value = temperature;
            // Update label if needed
        }

        public void MarkItem(ItemProperties item, bool isSelected)
        {
            // Highlight item green or red
        }

        public void ShowSuccess()
        {
            _goNextModulePanel.SetActive(true);
            _checkButton.SetActive(false);
            DelayFrameCall(DisableAllInteractionExceptGoNext).Forget();
        }

        public void ShowCompletion()
        {
            // Show final completion message
        }

        private async UniTaskVoid DelayFrameCall(Action action)
        {
            await UniTask.DelayFrame(2);
            action.Invoke();
        }
    }
}