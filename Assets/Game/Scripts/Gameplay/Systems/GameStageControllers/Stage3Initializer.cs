using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage3Initializer : StageInitializer
    {
        [Inject] private GardenViewController _gardenView;

        public override void InitGameView()
        {
            base.InitGameView();
            _gardenView.ShowDarkGarden();
            _gardenView.ShowSeparatePlant();
        }
    }
}