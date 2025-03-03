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
        private LifecycleManager _lifecycleManager;

        [SerializeField] private string _shooterSceneName;
        [SerializeField] private CharacterDialogueComponent _scientistCharacterCompomnent;

        private SaveLoadManager _saveLoadManager;

        [Inject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
            _saveLoadManager.LoadGame();
            //_lifecycleManager = lifecycleManager;
        }

        private void Start()
        {
            _scientistCharacterCompomnent.StartCurrentDialogueGroup();
        }

        public void GoToShooterScene()
        {
            _saveLoadManager.SaveGame();
            SceneManager.LoadScene(_shooterSceneName);
        }

        [Button]
        public void ResetGame()
        {
            _saveLoadManager.ResetGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}