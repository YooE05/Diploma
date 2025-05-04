using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class AverageNotesAndConclusionUI : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _conclusionPopupGO;
        private Tween _conclusionAnimation;

        [SerializeField] private GameObject _averageNotesPanelGO;
        [SerializeField] private List<GameObject> _notesGO = new();
        [SerializeField] private GameObject _averageNote;

        private Tween _notesAnimation;
        private Sequence _mySequence;

        public void OnInit()
        {
            _averageNotesPanelGO.SetActive(false);
            _conclusionPopupGO.SetActive(false);
        }

        [Button]
        public void ShowAverageNotes()
        {
            _mySequence = DOTween.Sequence();
            for (var i = 0; i < _notesGO.Count; i++)
            {
                _mySequence.Append(_notesGO[i].transform.DOScale(0.62f, 0.5f).From(0));
            }

            _mySequence.Append(_averageNote.transform.DOScale(1, 1f).From(0));

            _averageNotesPanelGO.SetActive(true);
            _mySequence.Play();
        }

        [Button]
        public void HideAverageNotes()
        {
            _mySequence.Kill(true);
            _averageNotesPanelGO.SetActive(false);
        }

        [Button]
        public void ShowConclusionPopup()
        {
            _conclusionPopupGO.SetActive(true);

            _conclusionAnimation = _conclusionPopupGO.transform.DOScale(1, 0.5f).From(0).SetEase(Ease.InOutSine)
                .SetAutoKill(false);
            _conclusionAnimation.Play();
        }

        [Button]
        public void HideConclusionPopup()
        {
            _conclusionAnimation.Complete();
            _conclusionAnimation.OnComplete(() =>
            {
                _averageNotesPanelGO.SetActive(false);
                Debug.Log("hide animation killed");
                _conclusionAnimation.Kill();
            }).PlayBackwards();
        }
    }
}