using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TutorialManager : MonoBehaviour, Listeners.IStartListener
    {
        [SerializeField] private GameObject _weaponPanel;
        [SerializeField] private GameObject _weaponInfo;

        [SerializeField] private ButtonView _nextButton;
        [SerializeField] private TutorialZone _weaponTutorialZone;

        public void OnStart()
        {
            _weaponInfo.SetActive(false);
            _weaponPanel.SetActive(false);

            _weaponTutorialZone.OnPlayerEnter += DelayedShowWeaponTutorial;
        }

        private void DelayedShowWeaponTutorial()
        {
            _weaponTutorialZone.OnPlayerEnter -= DelayedShowWeaponTutorial;
            ShowWeaponPanel().Forget();
        }

        private async UniTaskVoid ShowWeaponPanel()
        {
            await UniTask.WaitForSeconds(0.5f);

            _nextButton.OnButtonClicked += HideWeaponInfoPanel;

            _weaponPanel.transform.DOMoveY(-153.84f, 0f).SetLink(_weaponPanel).Play();
            _weaponPanel.SetActive(true);
            _weaponPanel.transform.DOMoveY(58f, 1f).SetLink(_weaponPanel).SetEase(Ease.InElastic).Play();

            _weaponInfo.transform.DOScale(0f, 0f).SetLink(_weaponInfo).Play();
            _weaponInfo.SetActive(true);
            _weaponInfo.transform.DOScale(0.81f, 0.6f).SetLink(_weaponInfo).Play();

            await UniTask.WaitForSeconds(1f);
            Time.timeScale = 0f;
        }

        private void HideWeaponInfoPanel()
        {
            _nextButton.OnButtonClicked -= HideWeaponInfoPanel;
            Time.timeScale = 1f;

            _weaponInfo.transform.DOScale(0f, 0.5f).SetLink(_weaponInfo)
                .OnComplete(() => { _weaponInfo.SetActive(false); }).Play();
        }
    }
}