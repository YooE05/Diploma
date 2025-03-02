using System.Collections.Generic;

namespace YooE.DialogueSystem
{
    public sealed class CharacterDialogueDataCollector
    {
        private List<CharacterDialogueComponent> _characters;

        public CharacterDialogueDataCollector(List<CharacterDialogueComponent> characters)
        {
            _characters = characters;
        }

        public CharacterDialogueData[] GetCharactersData()
        {
            List<CharacterDialogueData> dataList = new();

            for (var i = 0; i < _characters.Count; i++)
            {
                var data = _characters[i].GetCharacterData();
                if (data.DialogueCharacterID != nameof(DialogueCharacterID.NoNeedToSave))
                {
                    dataList.Add(data);
                }
            }

            return dataList.ToArray();
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
    }
}