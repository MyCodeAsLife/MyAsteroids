using System;

namespace Asteroids
{
    internal static class GameState
    {
        private static bool _isPaused;
        private static bool _isFinished;

        public static event Action<bool> SwitchIsPause;
        public static event Action<bool> SwitchIsFinish;

        static GameState()
        {
            SwitchIsFinish += OnSwichedIsFinish;
        }

        public static bool IsPaused
        {
            get
            {
                return _isPaused;
            }

            set
            {
                if (_isFinished == false)
                {
                    _isPaused = value;
                    SwitchIsPause?.Invoke(value);
                }
            }
        }

        public static bool IsFinished
        {
            get
            {
                return _isFinished;
            }

            private set
            {
                SwitchIsFinish?.Invoke(value);
                _isFinished = value;
            }
        }

        public static void EndGame()
        {
            IsFinished = true;
        }

        public static void StartGame()
        {
            IsFinished = false;
            IsPaused = false;
        }

        private static void OnSwichedIsFinish(bool value)
        {
            if (value)
                IsPaused = value;
        }
    }
}