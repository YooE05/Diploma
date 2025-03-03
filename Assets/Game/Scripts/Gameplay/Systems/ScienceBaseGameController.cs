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

        private SaveLoadManager _saveLoadManager;

        [Inject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
            _saveLoadManager.LoadGame();
        }

        private void Start()
        {
            _scientistCharacterComponent.StartCurrentDialogueGroup();
        }

        public void GoToShooterScene()
        {
            _saveLoadManager.SaveGame();
            if (_scientistCharacterComponent.GetCharacterData().GroupIndex == 1)
            {
                SceneManager.LoadScene(_shooterTutorialSceneName);
            }
            else
            {
                SceneManager.LoadScene(_shooterSceneName);
            }
        }

        [Button]
        public void ResetGame()
        {
            _saveLoadManager.ResetGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}