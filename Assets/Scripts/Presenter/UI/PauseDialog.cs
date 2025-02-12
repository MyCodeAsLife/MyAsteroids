using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class PauseDialog : MonoBehaviour
    {
        [SerializeField] private Transform _pauseMenu;
        [SerializeField] private TextMeshProUGUI _pauseMenuText;
        [SerializeField] private InformationPanel _informationPanel;

        private RootAudioSystem _audioSystem;

        private void Awake()
        {
            _audioSystem = FindFirstObjectByType<RootAudioSystem>();
        }

        private void OnEnable()
        {
            GameState.SwitchPause += OnPauseMenuPress;
            OnPauseMenuPress(GameState.IsPaused);
        }

        private void OnDisable()
        {
            GameState.SwitchPause -= OnPauseMenuPress;
        }

        private void OnPauseMenuPress(bool value)
        {
            if (value)
                _audioSystem.PlayBackgroundMusic(Config.PauseMenuMusic);
            else
                _audioSystem.PlayBackgroundMusic(Config.GameMusic);

            _pauseMenuText.text = "¬аш счет: " + _informationPanel.PlayerScore;
            _pauseMenu.gameObject.SetActive(value);
        }

        public void OnClickRestartGame()
        {
            _audioSystem.PlaySoundOnButtonClick();
            GameState.StartGame();
            SceneManager.LoadScene("Game");                                                     // Magic
        }

        public void OnClickExitToMainMenu()
        {
            _audioSystem.PlaySoundOnButtonClick();
            _audioSystem.PlayBackgroundMusic(Config.MainMenuMusic);
            GameState.IsPaused = false;
            SceneManager.LoadScene("MainMenu");                                                     // Magic
        }
    }
}