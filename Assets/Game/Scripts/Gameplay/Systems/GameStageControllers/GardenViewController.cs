using UnityEngine;
using YooE.Diploma.Interaction;

namespace YooE.Diploma
{
    public sealed class GardenViewController : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _badGardenGO;
        [SerializeField] private GameObject _separatePlantGO;

        [SerializeField] private GameObject _emptyGardenGO;
        [SerializeField] private SeedPlantInteractionComponent _seedPlantInteraction;
        [SerializeField] private GameObject _plantedSeedsGO;
        [SerializeField] private GameObject _lightLeversGO;
        [SerializeField] private LightLeverInteractionComponent _leverInteraction;

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
        }

        //Stage3
        public void ShowSeparatePlant()
        {
            _separatePlantGO.SetActive(true);
        }

        public void ShowDarkGarden()
        {
            _badGardenGO.SetActive(true);
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
            _leverInteraction.EnableInteraction();
            _seedPlantInteraction.EnableInteraction();
        }

        public void HideLightLevers()
        {
            _lightLeversGO.SetActive(false);

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
            _plantedSeedsGO.SetActive(true);
        }
    }
}