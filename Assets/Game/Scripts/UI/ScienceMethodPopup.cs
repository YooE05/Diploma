using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ScienceMethodPopup : MonoBehaviour
{
    [SerializeField] private GameObject _popupGO;

    [SerializeField] private Image _observationImage;
    [SerializeField] private Image _hypothesisImage;
    [SerializeField] private Image _experimentImage;
    [SerializeField] private Image _analysisImage;
    [SerializeField] private Image _conclusionImage;

    [SerializeField] private Color _disableColour;
    [SerializeField] private Color _enableColour;

    private void Awake()
    {
        ResetAllImage();
        Hide();
    }

    [Button]
    public void Hide()
    {
        _popupGO.SetActive(false);
    }

    [Button]
    public void Show()
    {
        _popupGO.SetActive(true);
    }

    [Button]
    public void ResetAllImage()
    {
        _observationImage.color = _disableColour;
        _hypothesisImage.color = _disableColour;
        _experimentImage.color = _disableColour;
        _analysisImage.color = _disableColour;
        _conclusionImage.color = _disableColour;
    }

    [Button]
    public void EnableObservation()
    {
        ResetAllImage();
        _observationImage.color = _enableColour;
    }

    [Button]
    public void EnableHypothesis()
    {
        ResetAllImage();
        _observationImage.color = _enableColour;
        _hypothesisImage.color = _enableColour;
    }

    [Button]
    public void EnableExperiment()
    {
        ResetAllImage();
        _observationImage.color = _enableColour;
        _hypothesisImage.color = _enableColour;
        _experimentImage.color = _enableColour;
    }

    [Button]
    public void EnableAnalysis()
    {
        ResetAllImage();
        _observationImage.color = _enableColour;
        _hypothesisImage.color = _enableColour;
        _experimentImage.color = _enableColour;
        _analysisImage.color = _enableColour;
    }

    [Button]
    public void EnableConclusion()
    {
        _observationImage.color = _enableColour;
        _hypothesisImage.color = _enableColour;
        _experimentImage.color = _enableColour;
        _analysisImage.color = _enableColour;
        _conclusionImage.color = _enableColour;
    }
}