using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class PlayerMovementInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;
        [SerializeField] private PlayerMovementConfig _movementConfig;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().AsSingle().WithArguments(_playerView.CharacterController, _movementConfig)
                .NonLazy();
            Container.Bind<PlayerRotation>().AsSingle().WithArguments(_playerView.Visual, _movementConfig.RotationSpeed)
                .NonLazy();
            Container.Bind<PlayerAnimation>().AsSingle()
                .WithArguments(_playerView.Animator, _movementConfig.AnimationsBlendSpeed).NonLazy();
            Container.BindInterfacesTo<PlayerMotionController>().AsSingle()
                .WithArguments(_playerView.Camera, _playerLocomotionInput).NonLazy();
        }
    }
}