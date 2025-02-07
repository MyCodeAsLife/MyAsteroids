using UnityEngine;

namespace Asteroids
{
    public class PauseMenu : MonoBehaviour
    {
        private RootController _rootController;
        private SingleReactiveProperty<bool> _isPause = new();

        public bool IsPause
        {
            get
            {
                return _isPause.Value;
            }

            private set
            {
                _isPause.Value = value;
            }
        }

        private void Awake()
        {
            _rootController = FindFirstObjectByType<RootController>();

        }

        private void OnEnable()
        {
            _rootController.PauseMenuPressed += OnPauseMenuPress;
        }

        private void OnPauseMenuPress()
        {
            _isPause.Value = !_isPause.Value;
        }
    }
}