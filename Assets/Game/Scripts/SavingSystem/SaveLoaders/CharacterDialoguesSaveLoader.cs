using YooE.DialogueSystem;

namespace YooE.SaveLoad
{
    public sealed class CharacterDialoguesSaveLoader : DataSaveLoader<CharacterDialogueData[], CharactersDataContainer>
    {
        protected override CharacterDialogueData[] GetData(CharactersDataContainer service)
        {
            return service.GetCharactersData();
        }

        protected override void SetData(CharactersDataContainer service, CharacterDialogueData[] data)
        {
            service.SetCharactersData(data);
        }

        protected override void SetDefaultData(CharactersDataContainer service)
        {
            service.SetDefaultData();
        }
    }
}