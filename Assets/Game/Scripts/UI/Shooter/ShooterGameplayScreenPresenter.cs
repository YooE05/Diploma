using System;

namespace YooE.Diploma
{
    public sealed class ShooterGameplayScreenPresenter : Listeners.IFinishListener, IDisposable,
        Listeners.IStartListener
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
            _enemiesInitializer.OnLiveEnemiesCountChanged += SetNewTotalDefeatEnemyCount;
        }

        private void SetNewTotalDefeatEnemyCount(int _1, int _2, int totalDefeat)
        {
            _gameplayScreenView.UpdateScoreText(totalDefeat);
        }

        private void SetEnemySliderValue(int deadCount, int allCount, int _)
        {
            if (allCount == 0)
            {
                _gameplayScreenView.UpdateEnemySlider(0);
            }
            else
            {
                _gameplayScreenView.UpdateEnemySlider(deadCount / (float)allCount * 100);
            }
        }

        public void OnStart()
        {
            _gameplayScreenView.SetUpRecordText(_enemiesInitializer.BestScore);
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