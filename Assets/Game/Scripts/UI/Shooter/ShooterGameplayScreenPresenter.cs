using System;
using Audio;
using UniRx;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterGameplayScreenPresenter : Listeners.IFinishListener, IDisposable
    {
        //private readonly CompositeDisposable _disposables = new();
        private readonly ShooterGameplayScreenView _gameplayScreenView;
        private readonly EnemyBrainsInitializer _enemyBrainsInitializer;

        private SoundButtonPresenter _soundButtonPresenter;

        public ShooterGameplayScreenPresenter(ShooterGameplayScreenView gameplayScreenView,
            EnemyBrainsInitializer enemyBrainsInitializer)
        {
            _gameplayScreenView = gameplayScreenView;

            _enemyBrainsInitializer = enemyBrainsInitializer;
            _enemyBrainsInitializer.OnLiveEnemiesCountChanged += SetEnemySliderValue;
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
            _enemyBrainsInitializer.OnLiveEnemiesCountChanged -= SetEnemySliderValue;
        }

        /*~ShooterGameplayScreenPresenter()
        {
            _enemyBrainsInitializer.OnLiveEnemiesCountChanged -= SetEnemySliderValue;
            _disposables.Dispose();
        }*/
    }
}