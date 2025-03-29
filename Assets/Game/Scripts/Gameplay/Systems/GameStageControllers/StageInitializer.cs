using UnityEngine;

namespace YooE.Diploma
{
    public class StageInitializer : MonoBehaviour, IStageInitializer
    {
        [SerializeField] private Transform _mainNPC;
        [SerializeField] private Transform _mainNPCSpawnPoint;

        [SerializeField] private Transform _player;
        [SerializeField] private Transform _playerSpawnPoint;

        [SerializeField] private int _dialogueGroupIndex;

        public int GetIndex()
        {
            return _dialogueGroupIndex;
        }

        public virtual void InitGameView()
        {
            _mainNPC.transform.position = _mainNPCSpawnPoint.position;
            _mainNPC.transform.rotation = _mainNPCSpawnPoint.rotation;

            _player.transform.position = _playerSpawnPoint.position;
            _player.transform.rotation = _playerSpawnPoint.rotation;
        }
    }
}