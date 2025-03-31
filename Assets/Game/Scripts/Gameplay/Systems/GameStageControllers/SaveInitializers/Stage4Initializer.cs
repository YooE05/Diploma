using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage4Initializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;

        public override void InitGameView()
        {
            base.InitGameView();
            _gardenView.ShowEmptyGarden();
            _gardenView.ShowLightLevers();

            _fightZoneInteraction.DisableInteractionAbility();
        }
    }
}