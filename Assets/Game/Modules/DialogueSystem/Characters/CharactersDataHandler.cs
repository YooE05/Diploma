using System.Collections.Generic;
using Utils;

namespace YooE.DialogueSystem
{
    public sealed class CharactersDataHandler
    {
        private readonly List<CharacterDialogueComponent> _characters = new();
        private readonly List<CharacterDialogueData> _charactersData = new();

        public void AddCharacters(List<CharacterDialogueComponent> characters)
        {
            _characters.Clear();
            _characters.AddRange(characters);
            SetDefaultData();

            for (var i = 0; i < characters.Count; i++)
            {
                var charId = characters[i].GetCharacterData().DialogueCharacterID;
                var characterDialogueData = _charactersData.Find(existChar => existChar.DialogueCharacterID == charId);
                if (characterDialogueData == null)
                {
                    _charactersData.Add(characters[i].GetCharacterData());
                }
            }

            SetCharactersData(_charactersData.ToArray());
        }

        public void SetNextCharacterDialogueGroup(DialogueCharacterID characterID)
        {
            var charData = _charactersData.Find(existChar =>
                existChar.DialogueCharacterID == EnumUtils<DialogueCharacterID>.ToString(characterID));

            if (charData != null)
            {
                charData.GroupIndex++;
            }
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
            _charactersData.Clear();
            _charactersData.AddRange(dataList);

            SetCharactersData(_charactersData.ToArray());
        }

        public void SetCharactersData(CharacterDialogueData[] dataList)
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

        public void SetDefaultData()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characters[i].SetGroupIndex(0);
            }
        }
    }
}