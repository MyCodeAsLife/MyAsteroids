using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Transform _pauseMenu;
        private RootAudioSystem _audioSystem;

        private void Awake()
        {
            _audioSystem = FindFirstObjectByType<RootAudioSystem>();
        }

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
            if (value)
                _audioSystem.PlayBackgroundMusic(Config.PauseMenuMusic);
            else
                _audioSystem.PlayBackgroundMusic(Config.GameMusic);

            _pauseMenu.gameObject.SetActive(value);
        }

        public void RestartGame()
        {
            GameState.IsPaused.Value = false;
            SceneManager.LoadScene("Game");                                                     // Magic
        }

        public void ExitToMainMenu()
        {
            GameState.IsPaused.Value = false;
            _audioSystem.PlayBackgroundMusic(Config.MainMenuMusic);
            SceneManager.LoadScene("MainMenu");                                                     // Magic
        }
    }
}