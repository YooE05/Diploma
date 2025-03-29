using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ScienceBaseGameController : MonoBehaviour
    {
        [SerializeField] private string _shooterSceneName;
        [SerializeField] private string _shooterTutorialSceneName;
        [SerializeField] private CharacterDialogueComponent _scientistCharacterComponent;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private StagesManger _stagesManger;

        private SaveLoadManager _saveLoadManager;
        private AudioManager _audioManager;
        private LifecycleManager _lifecycleManager;

        [Inject]
        public void Construct(LifecycleManager lifecycleManager, SaveLoadManager saveLoadManager,
            ScienceMethodPopup scienceMethodPopup, AudioManager audioManager)
        {
            _lifecycleManager = lifecycleManager;
            _audioManager = audioManager;
            _saveLoadManager = saveLoadManager;
            //scienceMethodPopup.Hide();
        }

        private void Start()
        {
            _saveLoadManager.OnDataLoaded += StartGameplay;
            _saveLoadManager.LoadGame();
        }

        private void StartGameplay()
        {
            _stagesManger.InitGameViewBySave();
            _saveLoadManager.OnDataLoaded -= StartGameplay;
            _audioManager.PlaySound(_audioClip, AudioOutput.Music);
            //_scientistCharacterComponent.StartCurrentDialogueGroup().Forget();
            _lifecycleManager.OnStart();
        }

        public void GoToShooterScene()
        {
            _audioManager.PlaySound(null, AudioOutput.Music);
            _saveLoadManager.SaveGame();

            SceneManager.LoadScene(_shooterSceneName);

            /*if (_scientistCharacterComponent.GetCharacterData().GroupIndex == 0)
            {
                SceneManager.LoadScene(_shooterTutorialSceneName);
            }
            else
            {
                SceneManager.LoadScene(_shooterSceneName);
            }*/
        }
        
        [Button]
        public void ResetGame()
        {
            _saveLoadManager.ResetGame();
            ReloadScene();
        }

        [Button]
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}