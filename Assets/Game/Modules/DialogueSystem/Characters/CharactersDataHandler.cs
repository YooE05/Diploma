using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace YooE.DialogueSystem
{
    public sealed class CharactersDataHandler
    {
        private readonly List<CharacterDialogueComponent> _characters = new();
        private readonly List<CharacterDialogueData> _charactersData = new();

        public void AddCharacters(List<CharacterDialogueComponent> characters)
        {
            //Debug.Log("AddChar");
            _characters.Clear();
            _characters.AddRange(characters);
            SetDefaultComponentsValue();

            for (var i = 0; i < characters.Count; i++)
            {
                var charId = characters[i].GetCharacterData().DialogueCharacterID;
                var characterDialogueData = _charactersData.Find(existChar => existChar.DialogueCharacterID == charId);
                if (characterDialogueData == null)
                {
                    _charactersData.Add(characters[i].GetCharacterData());
                }
            }

            SetupCharactersDialogueComponent(_charactersData.ToArray());
        }

        public void SetNextCharacterDialogueGroup(DialogueCharacterID characterID)
        {
            var charData = _charactersData.Find(existChar =>
                existChar.DialogueCharacterID == EnumUtils<DialogueCharacterID>.ToString(characterID));

            if (charData != null)
            {
                charData.GroupIndex++;
            }

            //Need Testing
            /*var characterDialogueComponent = _characters.Find(existChar =>
                existChar.GetCharacterData().DialogueCharacterID ==
                EnumUtils<DialogueCharacterID>.ToString(characterID));

            if (characterDialogueComponent != null)
            {
                characterDialogueComponent.SetNextDialogueGroup();
            }*/
        }

        public CharacterDialogueData[] GetCharactersData()
        {
            List<CharacterDialogueData> dataList = new();

            for (var i = 0; i < _charactersData.Count; i++)
            {
                var data = _charactersData[i];
                if (data.DialogueCharacterID != nameof(DialogueCharacterID.NoNeedToSave))
                {
                    dataList.Add(data);
                }
            }

            return dataList.ToArray();
        }

        public void SetCharactersDataFromSave(CharacterDialogueData[] dataList)
        {
            Debug.Log("SetSave");
            for (var i = 0; i < dataList.Length; i++)
            {
                var charId = dataList[i].DialogueCharacterID;
                var characterDialogueData = _charactersData.Find(existChar => existChar.DialogueCharacterID == charId);
                if (characterDialogueData == null)
                {
                    _charactersData.Add(dataList[i]);
                }
                else
                {
                    characterDialogueData.GroupIndex = dataList[i].GroupIndex;
                }
            }

            SetupCharactersDialogueComponent(_charactersData.ToArray());
        }

        public void UpdateCharacterDialogueIndex(DialogueCharacterID characterID)
        {
            var characterDialogueComponent = _characters.Find(existChar =>
                existChar.GetCharacterData().DialogueCharacterID ==
                EnumUtils<DialogueCharacterID>.ToString(characterID));

            if (characterDialogueComponent != null)
            {
                var charData = _charactersData.Find(existChar =>
                    existChar.DialogueCharacterID == EnumUtils<DialogueCharacterID>.ToString(characterID));

                characterDialogueComponent.SetGroupIndex(charData.GroupIndex);
            }
        }

        public int GetCharacterDialogueIndex(DialogueCharacterID characterID)
        {
            var charData = _charactersData.Find(existChar =>
                existChar.DialogueCharacterID == EnumUtils<DialogueCharacterID>.ToString(characterID));

            return charData?.GroupIndex ?? 0;
        }

        public CharacterDialogueComponent GetCharacterDialogueComponent(DialogueCharacterID characterID)
        {
            var charComponent = _characters.Find(existChar =>
                existChar.GetCharacterData().DialogueCharacterID ==
                EnumUtils<DialogueCharacterID>.ToString(characterID));

            return charComponent ? charComponent : null;
        }

        private void SetupCharactersDialogueComponent(CharacterDialogueData[] dataList)
        {
            for (var i = 0; i < dataList.Length; i++)
            {
                var character = _characters.Find(ch =>
                    ch.GetCharacterData().DialogueCharacterID == dataList[i].DialogueCharacterID);
                if (character != null)
                {
                    character.SetGroupIndex(dataList[i].GroupIndex);
                }
            }
        }

        public void SetDefaultDataFromSave()
        {
            Debug.Log("SetDefaultSave");

            SetDefaultComponentsValue();
            _charactersData.Clear();

            for (var i = 0; i < _characters.Count; i++)
            {
                var charId = _characters[i].GetCharacterData().DialogueCharacterID;
                var characterDialogueData = _charactersData.Find(existChar => existChar.DialogueCharacterID == charId);
                if (characterDialogueData == null)
                {
                    _charactersData.Add(_characters[i].GetCharacterData());
                }
            }
        }

        private void SetDefaultComponentsValue()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characters[i].SetGroupIndex(0);
            }
        }
    }
}