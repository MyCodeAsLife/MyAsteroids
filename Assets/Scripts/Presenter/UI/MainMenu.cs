using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class MainMenu : MonoBehaviour
    {
        private RootAudioSystem _audioSystem;
        private EventSystem _eventSystem;

        private void Awake()
        {
            _audioSystem = FindFirstObjectByType<RootAudioSystem>();
            _audioSystem.PlayBackgroundMusic(Config.MainMenuMusic);
            _eventSystem = FindFirstObjectByType<EventSystem>();
        }

        private void LateUpdate()
        {
            if (_eventSystem.IsPointerOverGameObject())
                _audioSystem.PlaySoundOnButtonHighlighted();
        }

        public void PlayGame()
        {
            _audioSystem.PlayBackgroundMusic(Config.GameMusic);
            SceneManager.LoadScene("Game");                             // Magic
        }

        public void ExitGame()
        {
            Application.Quit();         // В среде разработки не работает
        }
    }
}