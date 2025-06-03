using System;
using System.Collections.Generic;
using System.Linq;
using Game.Tutorial.Gameplay;
using TMPro;
using UnityEngine;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class StoreManager : MonoBehaviour, Listeners.IStartListener
    {
        public event Action OnStoreClosed;

        [SerializeField] private GameObject _storeInteraction;

        [SerializeField] private ButtonView _closeButton;
        [SerializeField] private GameObject _storePanel;
        [SerializeField] private GameObject _warningPanel;
        [SerializeField] private GameObject _moneyPanel;
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private NavigationManager _navigation;

        [SerializeField] private List<StoreItemButton> _itemsList;
        private PlayerDataContainer _playerDataContainer;
        private SaveLoadManager _saveLoadManager;

        [Inject]
        public void Construct(PlayerDataContainer playerDataContainer, SaveLoadManager saveLoadManager)
        {
            _playerDataContainer = playerDataContainer;
            _saveLoadManager = saveLoadManager;

            _closeButton.OnButtonClicked += CloseStore;
        }

        public void OpenStore()
        {
            _navigation.Stop();

            _warningPanel.SetActive(false);
            _storePanel.SetActive(true);

            for (var i = 0; i < _itemsList.Count; i++)
            {
                _itemsList[i].OnCurrentButtonClicked += ItemButtonClick;
            }
        }

        private void CloseStore()
        {
            _storePanel.SetActive(false);

            for (var i = 0; i < _itemsList.Count; i++)
            {
                _itemsList[i].OnCurrentButtonClicked -= ItemButtonClick;
            }

            OnStoreClosed?.Invoke();

            _playerDataContainer.UnlockedStoreItemsID = _itemsList.Where(item => item.IsItemUnlocked)
                .Select(item => item.Id)
                .ToList();

            _playerDataContainer.EnabledStoreItemsID = _itemsList
                .Where(item => item.IsItemUnlocked && item.IsItemEnabled)
                .Select(item => item.Id)
                .ToList();

            _saveLoadManager.SaveGame();
        }

        private void ItemButtonClick(ButtonView button)
        {
            _warningPanel.SetActive(false);

            var itemStoreButton = button as StoreItemButton;

            if (itemStoreButton != null && !itemStoreButton.IsItemUnlocked)
            {
                if (itemStoreButton.TryBuy(_playerDataContainer.CurrentMoney, out var remainMoney))
                {
                    //Sell sound
                    _moneyText.text = $"{remainMoney}";
                    _playerDataContainer.CurrentMoney = remainMoney;
                }
                else
                {
                    //Warning
                    _warningPanel.SetActive(true);
                }

                return;
            }

            if (itemStoreButton != null && itemStoreButton.IsItemUnlocked)
            {
                itemStoreButton.SwitchItemEnabling();
            }
        }

        public void OnStart()
        {
            _storePanel.SetActive(false);

            _storeInteraction.SetActive(_playerDataContainer.IsGameCompleted);
            _moneyPanel.SetActive(_playerDataContainer.IsGameCompleted);
            _moneyText.text = $"{_playerDataContainer.CurrentMoney}";

            //Init Items
            for (var i = 0; i < _itemsList.Count; i++)
            {
                _itemsList[i].InitItem(false);
            }

            for (var i = 0; i < _playerDataContainer.UnlockedStoreItemsID.Count; i++)
            {
                var storeItemButton =
                    _itemsList.Find(item => item.Id == _playerDataContainer.UnlockedStoreItemsID[i]);
                storeItemButton.InitItem(true);
            }

            for (var i = 0; i < _playerDataContainer.EnabledStoreItemsID.Count; i++)
            {
                var storeItemButton =
                    _itemsList.Find(item => item.Id == _playerDataContainer.EnabledStoreItemsID[i]);
                storeItemButton.InitItem(true, true);
            }
        }
    }
}