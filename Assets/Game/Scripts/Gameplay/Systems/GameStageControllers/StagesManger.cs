using System.Collections.Generic;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public sealed class StagesManger : MonoBehaviour
    {
        [SerializeField] private List<StageInitializer> _stageInitializers = new();

        private CharactersDataHandler _charactersDataHandler;

        [Inject]
        public void Construct(CharactersDataHandler charactersDataHandler)
        {
            _charactersDataHandler = charactersDataHandler;
        }

        public void InitGameView(int mainDialogueGroupIndex)
        {
            for (var i = 0; i < _stageInitializers.Count; i++)
            {
                if (_stageInitializers[i].GetIndex() != mainDialogueGroupIndex) continue;

                _stageInitializers[i].InitGameView();
                return;
            }
        }

        public void InitGameViewBySave()
        {
            var groupIndex = _charactersDataHandler.GetCharacterDialogueIndex(DialogueCharacterID.MainScientist);
            InitGameView(groupIndex);
        }
    }
}