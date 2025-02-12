using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class MainMenu : MonoBehaviour
    {
        private RootAudioSystem _audioSystem;

        private void Awake()
        {
            _audioSystem = FindFirstObjectByType<RootAudioSystem>();
            _audioSystem.PlayBackgroundMusic(Config.MainMenuMusic);
        }

        public void OnClickPlayGame()
        {
            _audioSystem.PlaySoundOnButtonClick();
            _audioSystem.PlayBackgroundMusic(Config.GameMusic);
            SceneManager.LoadScene("Game");                             // Magic
        }

        public void OnClickSettings()
        {
            _audioSystem.PlaySoundOnButtonClick();
        }

        public void OnClickExitGame()
        {
            _audioSystem.PlaySoundOnButtonClick();
            Application.Quit();         // В среде разработки не работает
        }

        public void OnClickAbout()
        {
            _audioSystem.PlaySoundOnButtonClick();
        }
    }
}