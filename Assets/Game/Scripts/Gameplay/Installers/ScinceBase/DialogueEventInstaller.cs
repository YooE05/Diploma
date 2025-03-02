using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public class DialogueEventInstaller : MonoInstaller
    {
        [SerializeField] private CubeHandler _cubeHandler;
        [SerializeField] private List<DSDialogueSO> _showCubeDialogues;

        public override void InstallBindings()
        {
            Container.Bind<CubeHandler>().FromInstance(_cubeHandler).AsCached().NonLazy();
            Container.Bind<ShowHideCubeEventController>().AsCached().WithArguments(_showCubeDialogues).NonLazy();
        }
    }
}