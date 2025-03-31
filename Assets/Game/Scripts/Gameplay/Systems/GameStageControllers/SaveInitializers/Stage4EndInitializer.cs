using UnityEngine;
using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage4EndInitializer : StageInitializer
    {
        [SerializeField] private GardenViewController _gardenView;

        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;

        public override void InitGameView()
        {
            base.InitGameView();

            _charactersTransform.MovePlayerToNPC();

            _gardenView.ShowEmptyGarden();
            _gardenView.ShowPlantedGarden();
            _gardenView.HideLightLevers();

            _fightZoneInteraction.EnableInteractionAbility();
        }
    }
}