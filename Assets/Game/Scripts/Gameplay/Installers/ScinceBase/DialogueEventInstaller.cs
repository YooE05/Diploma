using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public class DialogueEventInstaller : MonoInstaller
    {
        [SerializeField] private CubeController _cubeController;

        [SerializeField] private DSDialogueContainerSO _dialogueContainer;
        [SerializeField] private List<DSDialogueSO> _showCubeDialogues;

        public override void InstallBindings()
        {
            Container.Bind<DialogueState>().AsCached().WithArguments(_dialogueContainer).NonLazy();

            Container.Bind<CubeController>().FromInstance(_cubeController).AsCached().NonLazy();
            Container.Bind<ShowHideCubeEventController>().AsCached().WithArguments(_showCubeDialogues).NonLazy();
        }
    }
}