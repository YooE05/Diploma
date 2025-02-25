using YooE.Diploma;

namespace YooE
{
    public class UpdateTimer : Listeners.IUpdateListener
    {
        public float CurrentTime { get; private set; }

        private bool _isEnable;

        public void OnUpdate(float deltaTime)
        {
            if (!_isEnable) return;

            CurrentTime += deltaTime;
        }

        public void RestartTimer()
        {
            CurrentTime = 0;
            _isEnable = true;
        }

        public void StopTimer()
        {
            _isEnable = false;
        }
    }
}