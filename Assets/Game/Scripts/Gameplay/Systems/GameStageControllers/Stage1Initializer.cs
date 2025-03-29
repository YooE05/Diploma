using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage1Initializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;

        public override void InitGameView()
        {
            base.InitGameView();
            _charactersTransform.MovePlayerToNPC();
        }
    }
}