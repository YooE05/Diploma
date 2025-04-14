using Zenject;

namespace YooE.Diploma
{
    public sealed class EndStageInitializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;

        [Inject]
        public void Construct(GardenViewController gardenView)
        {
            _gardenView = gardenView;
        }

        public override void InitGameView()
        {
            _gardenView.ShowEndStageGarden();
        }
    }
}