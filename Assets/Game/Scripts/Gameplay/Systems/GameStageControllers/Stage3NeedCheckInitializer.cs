using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage3NeedCheckInitializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;
        [Inject] private Stage3TaskTracker _taskTracker;

        public override void InitGameView()
        {
            base.InitGameView();

            _gardenView.ShowDarkGarden();
            _gardenView.ShowSeparatePlant();
            _gardenView.EnableSeparatePlantAndDarkGardenInteraction();

            _taskTracker.ShowTasksText();
            _charactersTransform.MovePlayerToNPC();
            _fightZoneInteraction.DisableInteractionAbility();
        }
    }
}