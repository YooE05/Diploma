using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class MeasureInteractionComponent : InteractionComponent
    {
      //  [Inject] private Stage5TaskTracker _stage4TaskTracker;
        [Inject] private GardenViewController _gardenViewController;

        public override void Interact()
        {
            base.Interact();
            //Show Animation
            /*_stage4TaskTracker.PlantSeeds();
            _gardenViewController.ShowPlantedGarden();

            DisableInteractionAbility();
            DisableInteractView();*/
        }
    }
}