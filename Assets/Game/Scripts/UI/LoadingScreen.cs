using DG.Tweening;
using UnityEngine;

namespace YooE.Diploma
{
    public class LoadingScreen : MonoBehaviour//, Listeners.IInitListener
    {
        [SerializeField] private GameObject _wheel;

        private Tween _wheelRotation;

        /*
        public void Awake()
        {
            _wheelRotation = _wheel.transform.DORotate(new Vector3(0f, 0f, 360f), 6f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetLink(_wheel);
        }*/

        public void Show()
        {
            _wheelRotation ??= _wheel.transform.DORotate(new Vector3(0f, 0f, 360f), 6f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetLink(_wheel);
            
            _wheelRotation.Play();
            gameObject.SetActive(true);
        }


        public void Hide()
        {
            _wheelRotation.Pause();
            gameObject.SetActive(false);
        }
    }
}