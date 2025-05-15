using System;

namespace YooE.Diploma
{
    public sealed class ShooterGameplayScreenPresenter : Listeners.IFinishListener, IDisposable
    {
        //private readonly CompositeDisposable _disposables = new();
        private readonly ShooterGameplayScreenView _gameplayScreenView;
        private readonly EnemiesInitializer _enemiesInitializer;

        private SoundButtonPresenter _soundButtonPresenter;

        public ShooterGameplayScreenPresenter(ShooterGameplayScreenView gameplayScreenView,
            EnemiesInitializer enemiesInitializer)
        {
            _gameplayScreenView = gameplayScreenView;

            _enemiesInitializer = enemiesInitializer;
            _enemiesInitializer.OnLiveEnemiesCountChanged += SetEnemySliderValue;
        }

        private void SetEnemySliderValue(int deadCount, int allCount)
        {
            _gameplayScreenView.UpdateEnemySlider(deadCount / (float)allCount * 100);
        }

        public void OnFinish()
        {
            HideScreenView();
        }

        public void HideScreenView()
        {
            _gameplayScreenView.Hide();
        }

        public void Dispose()
        {
            _enemiesInitializer.OnLiveEnemiesCountChanged -= SetEnemySliderValue;
        }

        /*~ShooterGameplayScreenPresenter()
        {
            _enemyBrainsInitializer.OnLiveEnemiesCountChanged -= SetEnemySliderValue;
            _disposables.Dispose();
        }*/
    }
}