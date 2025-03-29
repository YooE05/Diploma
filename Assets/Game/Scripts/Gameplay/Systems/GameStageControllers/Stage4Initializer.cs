using UnityEngine;

namespace YooE.Diploma
{
    public sealed class Stage4Initializer : StageInitializer
    {
        [SerializeField] private GardenViewController _gardenView;

        public override void InitGameView()
        {
            base.InitGameView();
            _gardenView.ShowEmptyGarden();
            _gardenView.ShowLightLevers();
        }
    }
}