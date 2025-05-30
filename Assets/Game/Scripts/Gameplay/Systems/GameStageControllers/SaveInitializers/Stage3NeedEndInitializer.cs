using Game.Tutorial.Gameplay;
using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage3EndInitializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;
        [Inject] private NavigationManager _navigationManager;

        public override void InitGameView()
        {
            base.InitGameView();

            _gardenView.ShowDarkGarden();
            _gardenView.ShowSeparatePlant();

            _charactersTransform.MovePlayerToNPC();
            _fightZoneInteraction.EnableInteractionAbility();

            _navigationManager.SetNavigationToDoor();
        }
    }
}