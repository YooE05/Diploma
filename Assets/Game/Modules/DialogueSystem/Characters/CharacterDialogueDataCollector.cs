using System.Collections.Generic;
using UnityEngine;

namespace YooE.DialogueSystem
{
    public sealed class CharacterDialogueDataCollector
    {
        private readonly List<CharacterDialogueComponent> _characters;
        private readonly CharactersDataContainer _charactersDataContainer;

        public CharacterDialogueDataCollector(List<CharacterDialogueComponent> characters,
            CharactersDataContainer charactersDataContainer)
        {
            _characters = characters;
            _charactersDataContainer = charactersDataContainer;
            _charactersDataContainer.AddCharacterComponents(_characters);

            SetFirstForEveryCharacterGroup();
            SetupCharacters(_charactersDataContainer.GetCharactersData());
        }

        ~CharacterDialogueDataCollector()
        {
            _charactersDataContainer.RemoveCharacterComponents(_characters);
        }

        public void SetupCharacters(CharacterDialogueData[] dataList)
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

        public void SetFirstForEveryCharacterGroup()
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characters[i].SetGroupIndex(0);
            }
        }
    }

    public sealed class CharactersDataContainer
    {
        private CharacterDialogueData[] _charactersData;
        private readonly List<CharacterDialogueComponent> _characterDialogueComponents = new();

        public CharacterDialogueData[] CollectCharactersData()
        {
            List<CharacterDialogueData> dataList = new();

            for (var i = 0; i < _characterDialogueComponents.Count; i++)
            {
                var data = _characterDialogueComponents[i].GetCharacterData();
                if (data.DialogueCharacterID != nameof(DialogueCharacterID.NoNeedToSave))
                {
                    dataList.Add(data);
                }
            }

            return dataList.ToArray();
        }

        public CharacterDialogueData[] GetCharactersData()
        {
            return _charactersData;
        }

        public void AddCharacterComponents(List<CharacterDialogueComponent> components)
        {
            for (var i = 0; i < components.Count; i++)
            {
                var charId = components[i].GetCharacterData().DialogueCharacterID;
                if (!_characterDialogueComponents.Find(existChar =>
                        existChar.GetCharacterData().DialogueCharacterID == charId))
                {
                    _characterDialogueComponents.Add(components[i]);
                }
            }
        }

        public void RemoveCharacterComponents(List<CharacterDialogueComponent> components)
        {
            foreach (var c in components)
            {
                if (_characterDialogueComponents.Find(existChar =>
                        existChar.GetCharacterData().DialogueCharacterID == c.GetCharacterData().DialogueCharacterID))
                {
                    _characterDialogueComponents.Remove(c);
                }
            }
        }

        public void SetCharactersData(CharacterDialogueData[] dataList)
        {
            _charactersData = dataList;
        }

        public void SetDefaultData()
        {
            _charactersData = new CharacterDialogueData[] { };
        }
    }
}