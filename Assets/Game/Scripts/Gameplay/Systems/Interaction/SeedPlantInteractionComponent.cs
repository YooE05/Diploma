using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class SeedPlantInteractionComponent : InteractionComponent
    {
        [Inject] private Stage4TaskTracker _stage4TaskTracker;
        [Inject] private GardenViewController _gardenViewController;

        public override void Interact()
        {
            base.Interact();
            //Show Animation
            _stage4TaskTracker.PlantSeeds();
            _gardenViewController.ShowPlantedGarden();

            DisableInteractionAbility();
            DisableInteractView();
        }
    }
}