using System;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterEndZone : MonoBehaviour
    {
        public event Action OnFinish;

        private EnemiesInitializer _enemiesInitializer;
        [SerializeField] private float _finishPercent;
        [SerializeField] private GameObject _cantFinishPanel;

        private bool _canFinishLevel = false;

        [Inject]
        public void Construct(EnemiesInitializer enemiesInitializer)
        {
            _enemiesInitializer = enemiesInitializer;
            _enemiesInitializer.OnLiveEnemiesCountChanged += ChangeFinishAbility;
            _canFinishLevel = false;
            _cantFinishPanel.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (!_canFinishLevel) _cantFinishPanel.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _cantFinishPanel.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && _canFinishLevel)
            {
                FinishActions();
                OnFinish?.Invoke();
            }
        }

        private void FinishActions()
        {
            _cantFinishPanel.SetActive(false);
            _enemiesInitializer.OnLiveEnemiesCountChanged -= ChangeFinishAbility;
        }

        private void ChangeFinishAbility(int deadEnemiesCount, int enemiesCount)
        {
            if (deadEnemiesCount / (float)enemiesCount >= _finishPercent / 100f)
            {
                EnableFinish();
            }
        }

        private void EnableFinish()
        {
            //VFX
            _canFinishLevel = true;
        }
    }
}