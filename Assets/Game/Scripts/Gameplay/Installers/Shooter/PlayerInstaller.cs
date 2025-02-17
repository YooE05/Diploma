using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerMovementConfig _movementConfig;
        [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;

        [SerializeField] private GameObjectsPoolConfig _bulletsPoolConfig;
        [SerializeField] private Transform _bulletsInitParent;

        public override void InstallBindings()
        {
            Container.Bind<BulletsSystem>().AsSingle().WithArguments(_bulletsPoolConfig, _bulletsInitParent).NonLazy();
            Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle().NonLazy();
            // Container.BindInterfacesTo<PlayerShooting>().AsSingle().WithArguments(_playerView.WeaponView).NonLazy();

            Motion();
        }

        private void Motion()
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