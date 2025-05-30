using System;
using DG.Tweening;
using UnityEngine;
using YooE.Diploma;

namespace Game.Tutorial.Gameplay
{
    public sealed class NavigationArrow : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _rootGameObject;

        [SerializeField] private Transform _rootTransform;
        [SerializeField] private GameObject _canvasGO;

        private Tween _arrowMove;

        public void OnInit()
        {
            _arrowMove = _canvasGO.transform.DOLocalMoveZ(1.58f, 0.6f).From(1.087f).SetLoops(-1, LoopType.Yoyo)
                .SetLink(_canvasGO);
        }

        public void Show()
        {
            _arrowMove.Play();
            _rootGameObject.SetActive(true);
        }

        public void Hide()
        {
            _arrowMove.Pause();
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            _rootTransform.position = position;
        }

        public void LookAt(Vector3 targetPosition)
        {
            var distanceVector = targetPosition - _rootTransform.position;
            distanceVector.y = 0;
            _rootTransform.rotation = Quaternion.LookRotation(distanceVector.normalized, Vector3.up);
        }
    }
}