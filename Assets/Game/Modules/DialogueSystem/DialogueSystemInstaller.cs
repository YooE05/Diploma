using DS.ScriptableObjects;
using UnityEngine;
using Zenject;


namespace YooE.DialogueSystem
{
    public class DialogueSystemInstaller : MonoInstaller
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private DSDialogueContainerSO _dialogueContainer;

        public override void InstallBindings()
        {
            Container.Bind<DialogueView>().FromInstance(_dialogueView).AsCached().NonLazy();
            Container.Bind<DialogueState>().AsCached().WithArguments(_dialogueContainer).NonLazy();
        }
    }
}