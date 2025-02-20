using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterLevelInstaller : MonoInstaller
    {
        //[SerializeField] private CameraConfig _cameraConfig;
        //[SerializeField] private Camera _camera;
        //[SerializeField] private InputConfig _inputConfig;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LifecycleManager>().AsSingle().NonLazy();

            /*Container
                .Bind<Camera>()
                .FromInstance(_camera);

            Container
                .Bind<ICharacter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MoveController>()
                .AsCached()
                .NonLazy();

            Container
                .Bind<IMoveInput>()
                .To<MoveInput>()
                .AsSingle()
                .WithArguments(_inputConfig)
                .NonLazy();

            Container
                .BindInterfacesTo<CameraFollower>()
                .AsCached()
                .WithArguments(_cameraConfig.cameraOffset)
                .NonLazy();*/
        }
    }
}