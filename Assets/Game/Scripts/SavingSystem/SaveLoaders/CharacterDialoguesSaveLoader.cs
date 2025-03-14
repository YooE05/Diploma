using YooE.DialogueSystem;

namespace YooE.SaveLoad
{
    public sealed class CharacterDialoguesSaveLoader : DataSaveLoader<CharacterDialogueData[], CharactersDataHandler>
    {
        protected override CharacterDialogueData[] GetData(CharactersDataHandler service)
        {
            return service.GetCharactersData();
        }

        protected override void SetData(CharactersDataHandler service, CharacterDialogueData[] data)
        {
            service.SetCharactersDataFromSave(data);
        }

        protected override void SetDefaultData(CharactersDataHandler service)
        {
            service.SetDefaultDataFromSave();
        }
    }
}