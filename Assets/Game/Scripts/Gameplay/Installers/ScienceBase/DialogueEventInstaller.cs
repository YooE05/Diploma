using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class DialogueEventInstaller : MonoInstaller
    {
        [SerializeField] private List<DSDialogueSO> _showCubeDialogues;
        [SerializeField] private List<DSDialogueSO> _startShooterDialogues;
        [SerializeField] private List<DSDialogueSO> _enableMotionDialogues;

        public override void InstallBindings()
        {
            Container.Bind<ShowHideCubeEvent>().AsCached().WithArguments(_showCubeDialogues).NonLazy();
            Container.Bind<StartShooterEvent>().AsCached().WithArguments(_startShooterDialogues).NonLazy();
            Container.Bind<EnableMotionEvent>().AsCached().WithArguments(_enableMotionDialogues).NonLazy();
        }
    }
}