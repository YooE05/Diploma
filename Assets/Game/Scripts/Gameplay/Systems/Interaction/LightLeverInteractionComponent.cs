using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class LightLeverInteractionComponent : InteractionComponent
    {
        [Inject] private Stage4TaskTracker _stage4TaskTracker;
        [Inject] private GardenViewController _gardenViewController;


        public override void Interact()
        {
            base.Interact();
            //Show Animation
            _stage4TaskTracker.AdjustGardenLight();

            DisableInteractionAbility();
            DisableInteractView();
        }
    }
}