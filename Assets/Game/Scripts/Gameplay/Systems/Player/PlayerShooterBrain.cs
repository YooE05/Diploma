namespace YooE.Diploma
{
    public sealed class PlayerShooterBrain : Listeners.IFinishListener
    {
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerShooting _playerShooting;
        private readonly PlayerAnimation _playerAnimation;

        public AnimationEvents AnimationEvents => _playerAnimation.AnimationEvents;

        public bool IsDead { get; private set; }

        public PlayerShooterBrain(PlayerShooterView playerView,
            PlayerMotionController playerMotionController, PlayerShooting playerShooting,
            PlayerAnimation playerAnimation)
        {
            _hitPointsComponent = playerView.HitPointsComponent;
            _playerMotionController = playerMotionController;
            _playerShooting = playerShooting;
            _playerAnimation = playerAnimation;
            IsDead = false;
        }

        public void StartDeathActions()
        {
            _playerAnimation.SetAnimatorTrigger("IsDead");

            _playerMotionController.DisableMotion();
            _playerShooting.DisableShooting();
            IsDead = true;
        }

        public HitPointsComponent GetHitPointsComponent()
        {
            return _hitPointsComponent;
        }

        public void OnFinish()
        {
            WinActions();
        }

        public void WinActions()
        {
            _playerAnimation.SetAnimatorTrigger("IsWin");

            _playerMotionController.DisableMotion();
            _playerShooting.DisableShooting();
        }
    }
}