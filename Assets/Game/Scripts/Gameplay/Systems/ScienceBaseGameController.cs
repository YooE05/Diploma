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
        private PlayerDataContainer _playerDataContainer;
        private ShooterLoader _shooterLoader;

        [Inject]
        public void Construct(LifecycleManager lifecycleManager, SaveLoadManager saveLoadManager,
            ScienceMethodPopup scienceMethodPopup, AudioManager audioManager,
            PlayerDataContainer playerDataContainer, ShooterLoader shooterLoader)
        {
            _playerDataContainer = playerDataContainer;
            _lifecycleManager = lifecycleManager;
            _audioManager = audioManager;
            _saveLoadManager = saveLoadManager;

            _shooterLoader = shooterLoader;
        }

        private void Start()
        {
            _saveLoadManager.OnDataLoaded += StartGameplay;
            _saveLoadManager.LoadGame();
        }

        private void StartGameplay()
        {
            Debug.Log(_playerDataContainer.LastScore);
            Debug.Log(_playerDataContainer.BestScore);
            Debug.Log(_playerDataContainer.CurrentMoney);

            _stagesManger.InitGameViewBySave();
            _saveLoadManager.OnDataLoaded -= StartGameplay;
            _audioManager.PlaySound(_audioClip, AudioOutput.Music);

            /*if (_scientistCharacterComponent.GetCharacterData().GroupIndex == 0)
            {
                _scientistCharacterComponent.StartCurrentDialogueGroup().Forget();
            }*/

            _playerDataContainer.IsGameCompleted = _scientistCharacterComponent.GetCharacterData().GroupIndex > 20;

            _lifecycleManager.OnStart();
        }

        public void GoToShooterScene()
        {
            _audioManager.PlaySound(null, AudioOutput.Music);
            _saveLoadManager.SaveGame();

            // LoadShooterScene(_scientistCharacterComponent.GetCharacterData().GroupIndex);

            _shooterLoader.LoadShooterScene(_scientistCharacterComponent.GetCharacterData().GroupIndex);

            /*if (_scientistCharacterComponent.GetCharacterData().GroupIndex == 0)
            {
                SceneManager.LoadScene(_shooterTutorialSceneName);
            }
            else
            {
                SceneManager.LoadScene(_shooterSceneName);
            }*/
        }

        /*private void LoadShooterScene(int mainDialogueGroupIndex)
        {
            var sceneName = mainDialogueGroupIndex switch
            {
                0 => "ShooterTutorial",
                2 => "Shooter1",
                6 => "Shooter2",
                10 => "Shooter3",
                17 => "Shooter4",
                19 => "Shooter5",
                _ => "ShooterEndless"
            };

            SceneManager.LoadScene(sceneName);
        }*/

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