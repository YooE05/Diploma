using UnityEngine;

namespace YooE.Diploma
{
    public class CharactersTransformInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _mainNPC;
        [SerializeField] private Transform _mainNPCCommonPoint;
        [SerializeField] private Transform _mainNPCGardenPoint;

        [SerializeField] private Transform _player;
        [SerializeField] private Transform _playerNearNPCPoint;
        [SerializeField] private Transform _playerDoorPoint;
        [SerializeField] private Transform _playerLabCenterPoint;

        public void MoveCharactersToMainPoints()
        {
            _mainNPC.transform.position = _mainNPCCommonPoint.position;
            _mainNPC.transform.rotation = _mainNPCCommonPoint.rotation;

            _player.transform.position = _playerDoorPoint.position;
            //_player.transform.rotation = _playerDoorPoint.rotation;
        }

        public void MovePlayerToNPC()
        {
            _player.transform.position = _playerNearNPCPoint.position;
            //   _player.transform.rotation = _playerNearNPCPoint.rotation;
        }

        public void MoveMainNPCToGarden()
        {
            _mainNPC.transform.position = _mainNPCGardenPoint.position;
            _mainNPC.transform.rotation = _mainNPCGardenPoint.rotation;
        }

        public void MovePlayerToSceneCenter()
        {
            _player.transform.position = _playerLabCenterPoint.position;
            // _player.transform.rotation = _playerLabCenterPoint.rotation;
        }
    }
}