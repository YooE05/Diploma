using UnityEngine;
using YooE.Diploma.Interaction;

namespace YooE.Diploma
{
    public sealed class GardenViewController : MonoBehaviour, Listeners.IInitListener
    {
        //Stage3
        [SerializeField] private GameObject _badGardenGO;
        [SerializeField] private CharacterInteractionComponent _badGardenInteraction;
        [SerializeField] private GameObject _separatePlantGO;
        [SerializeField] private CharacterInteractionComponent _separatePlantInteraction;

        //Stage4
        [SerializeField] private GameObject _emptyGardenGO;
        [SerializeField] private SeedPlantInteractionComponent _seedPlantInteraction;
        [SerializeField] private GameObject _plantedSeedsGO;
        [SerializeField] private GameObject _lightLeversGO;
        [SerializeField] private LightLeverInteractionComponent _leverInteraction;

        //Stage5
        [SerializeField] private GameObject _grownGardenGO;
        [SerializeField] private MeasureInteractionComponent _grownGardenGOInteraction;

        public void OnInit()
        {
            HideView();
        }

        private void HideView()
        {
            _separatePlantGO.SetActive(false);
            _badGardenGO.SetActive(false);

            _emptyGardenGO.SetActive(false);
            _plantedSeedsGO.SetActive(false);
            _lightLeversGO.SetActive(false);

            _grownGardenGO.SetActive(false);
        }

        //Stage3
        public void ShowSeparatePlant()
        {
            _separatePlantGO.SetActive(true);
            _separatePlantInteraction.DisableInteractView();
            _separatePlantInteraction.DisableInteractionAbility();
        }

        public void ShowDarkGarden()
        {
            _badGardenGO.SetActive(true);
            _badGardenInteraction.DisableInteractView();
            _badGardenInteraction.DisableInteractionAbility();
        }

        public void EnableStage3Interaction()
        {
            _separatePlantInteraction.EnableInteractionAbility();
            _badGardenInteraction.EnableInteractionAbility();
        }

        public void DisableStage3Interaction()
        {
            _separatePlantInteraction.DisableInteractView();
            _separatePlantInteraction.DisableInteractionAbility();

            _badGardenInteraction.DisableInteractView();
            _badGardenInteraction.DisableInteractionAbility();
        }

        //Stage4
        public void ShowLightLevers()
        {
            _lightLeversGO.SetActive(true);

            _leverInteraction.DisableInteractView();
            _leverInteraction.DisableInteractionAbility();
        }

        public void EnableLeverAndGardenInteraction()
        {
            _leverInteraction.EnableInteractionAbility();
            _seedPlantInteraction.EnableInteractionAbility();
        }

        public void HideLightLevers()
        {
            // _lightLeversGO.SetActive(false);

            _leverInteraction.DisableInteractView();
            _leverInteraction.DisableInteractionAbility();
        }

        public void ShowEmptyGarden()
        {
            _emptyGardenGO.SetActive(true);

            _seedPlantInteraction.DisableInteractView();
            _seedPlantInteraction.DisableInteractionAbility();
        }

        public void ShowPlantedGarden()
        {
            _emptyGardenGO.SetActive(true);
            _plantedSeedsGO.SetActive(true);
        }

        public void ShowGrownGarden()
        {
            _grownGardenGO.SetActive(true);
        }

        //Stage5
        
        public void EnableStage5Interaction()
        {
            throw new System.NotImplementedException();
        }
    }
}