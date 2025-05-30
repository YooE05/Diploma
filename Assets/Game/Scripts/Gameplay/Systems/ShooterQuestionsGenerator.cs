using System;
using System.Collections.Generic;
using Audio;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterQuestionsGenerator : MonoBehaviour
    {
        public event Action OnGoodAnswer;
        public event Action OnBadAnswer;

        private DialogueState _dialogueState;
        private AudioManager _audioManager;

        // [SerializeField] private DialogueGroupsSequenceConfig _questions;
        [SerializeField] private List<DSDialogueSO> _questions;
        [SerializeField] private List<DSDialogueSO> _goodAnswers;
        [SerializeField] private List<DSDialogueSO> _badAnswers;

        [SerializeField] private ParticleSystem _correctAnswerParticles;

        [Inject]
        public void Construct(DialogueState dialogueState, AudioManager audioManager)
        {
            _dialogueState = dialogueState;

            _dialogueState.OnDialogueStart += OnDialogueStart;
            _dialogueState.OnDialogueFinished += OnDialogueFinish;

            _audioManager = audioManager;

            _correctAnswerParticles.Stop();
        }

        private void OnDialogueStart(DSDialogueSO dialogue)
        {
            if (_goodAnswers.Contains(dialogue))
            {
                ShowGoodAnswer();
            }

            if (_badAnswers.Contains(dialogue))
            {
                ShowBadAnswer();
            }
        }

        private void OnDialogueFinish(DSDialogueSO dialogue)
        {
            if (_goodAnswers.Contains(dialogue))
                CloseGoodAnswer();

            if (_badAnswers.Contains(dialogue))
                CloseBadAnswer();
        }

        public void ShowQuestion()
        {
            _dialogueState.StartDialogue(_questions.GetRandomItemsFisherYates(1)[0]);
        }

        private void ShowBadAnswer()
        {
            //_dialogueState.StartDialogue(_badAnswers.GetRandomItemsFisherYates(1)[0]);
        }

        private void ShowGoodAnswer()
        {
            //_dialogueState.StartDialogue(_badAnswers.GetRandomItemsFisherYates(1)[0]);
        }

        private void CloseBadAnswer()
        {
            if (_audioManager.TryGetAudioClipByName("wrongAnswer", out var audioClip))
            {
                _audioManager.PlaySoundOneShot(audioClip, AudioOutput.Master);
            }

            OnBadAnswer?.Invoke();
        }

        private void CloseGoodAnswer()
        {
            if (_audioManager.TryGetAudioClipByName("correctAnswer", out var audioClip))
            {
                _audioManager.PlaySoundOneShot(audioClip, AudioOutput.Master);
            }

            _correctAnswerParticles.Play();

            OnGoodAnswer?.Invoke();
        }
    }
}