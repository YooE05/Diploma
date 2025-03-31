using UnityEngine;
using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage5EndInitializer : StageInitializer
    {
        [SerializeField] private Stage5StartInitializer _startInitializer;
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;

        public override void InitGameView()
        {
            _startInitializer.InitGameView();

            _charactersTransform.MovePlayerToSceneCenter();
            _charactersTransform.MoveMainNPCToGarden();

            _fightZoneInteraction.EnableInteractionAbility();
        }
    }
}