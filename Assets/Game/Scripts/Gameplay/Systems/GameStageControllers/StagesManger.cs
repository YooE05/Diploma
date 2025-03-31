using System.Collections.Generic;
using UnityEngine;
using Utils;
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

            //For save indexes between main spots
            _stageInitializers.Sort((x, y) => x.GetIndex().CompareTo(y.GetIndex()));
            var needInitializerId = 0;
            for (var i = 0; i < _stageInitializers.Count; i++)
            {
                if (_stageInitializers[i].GetIndex() < mainDialogueGroupIndex)
                {
                    needInitializerId = i;
                    continue;
                }

                var charactersData = _charactersDataHandler.GetCharactersData();
                for (var j = 0; j < charactersData.Length; j++)
                {
                    if (charactersData[j].DialogueCharacterID !=
                        EnumUtils<DialogueCharacterID>.ToString(DialogueCharacterID.MainScientist)) continue;

                    charactersData[j].GroupIndex = _stageInitializers[needInitializerId].GetIndex();
                    break;
                }

                _charactersDataHandler.SetCharactersDataFromSave(charactersData);

                _stageInitializers[needInitializerId].InitGameView();
                return;
            }

            _stageInitializers[^1].InitGameView();
        }

        public void InitGameViewBySave()
        {
            var groupIndex = _charactersDataHandler.GetCharacterDialogueIndex(DialogueCharacterID.MainScientist);
            InitGameView(groupIndex);
        }
    }
}