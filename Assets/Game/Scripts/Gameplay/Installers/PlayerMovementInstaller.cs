using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class PlayerMovementInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerMovementConfig _movementConfig;
        [SerializeField] private LayerMask _noPlayerLayerMask;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
            Container.Bind<PlayerMovement>().AsSingle().WithArguments(_playerView.CharacterController, _movementConfig)
                .NonLazy();
            Container.Bind<PlayerRotation>().AsSingle().WithArguments(_playerView.Visual, _movementConfig.RotationSpeed)
                .NonLazy();
            Container.Bind<PlayerAnimation>().AsSingle()
                .WithArguments(_playerView.Animator, _movementConfig.AnimationsBlendSpeed, _playerView.AnimationEvents)
                .NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMotionController>().AsSingle()
                .WithArguments(_playerView.Camera, _playerView.Visual, _noPlayerLayerMask).NonLazy();
        }
    }
}