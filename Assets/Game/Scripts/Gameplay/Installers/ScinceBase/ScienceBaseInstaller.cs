using DS.ScriptableObjects;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;


namespace YooE.Diploma
{
    public class ScienceBaseInstaller : MonoInstaller
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private DSDialogueContainerSO _dialogueContainer;

        public override void InstallBindings()
        {
            DialogueSystem();
        }

        private void DialogueSystem()
        {
            Container.Bind<DialogueView>().FromInstance(_dialogueView).AsCached().NonLazy();
            Container.Bind<DialogueState>().AsCached().WithArguments(_dialogueContainer).NonLazy();
        }
    }
}