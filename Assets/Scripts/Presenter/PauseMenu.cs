using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Transform _pauseMenu;

        private void OnEnable()
        {
            GameState.IsPaused.Changed += OnPauseMenuPress;
            OnPauseMenuPress(GameState.IsPaused.Value);
        }

        private void OnDisable()
        {
            GameState.IsPaused.Changed -= OnPauseMenuPress;
        }

        private void OnPauseMenuPress(bool value)
        {
            _pauseMenu.gameObject.SetActive(GameState.IsPaused.Value);
        }

        public void RestartGame()
        {
            GameState.IsPaused.Value = false;
            SceneManager.LoadScene("Game");
        }

        public void ExitToMainMenu()
        {
            GameState.IsPaused.Value = false;
            SceneManager.LoadScene("Menu");
        }
    }
}