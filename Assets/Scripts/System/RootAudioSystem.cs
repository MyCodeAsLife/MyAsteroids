using UnityEngine;
using UnityEngine.Audio;

namespace Asteroids
{
    public class RootAudioSystem : MonoBehaviour
    {
        private AudioMixer _mixerGroup;
        private AudioSource _audioBackground;
        private AudioSource _audioExplosion;
        private AudioSource _audioFirstGun;
        private AudioSource _audioSecondGun;
        private AudioSource _audioUI;
        //private AudioClip _buttonHighlight;                  // Загрузить    Используется и в главном меню
        private AudioClip _buttonClick;
        private AudioClip _explosion1;
        private AudioClip _explosion2;
        private AudioClip _laserBeam;
        private AudioClip _blast;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _audioBackground = gameObject.AddComponent<AudioSource>();
            _audioExplosion = gameObject.AddComponent<AudioSource>();
            _audioFirstGun = gameObject.AddComponent<AudioSource>();
            _audioSecondGun = gameObject.AddComponent<AudioSource>();
            _audioUI = gameObject.AddComponent<AudioSource>();
            _explosion1 = Resources.Load<AudioClip>("Audio/Explosion1");
            _explosion2 = Resources.Load<AudioClip>("Audio/Explosion2");
            _blast = Resources.Load<AudioClip>("Audio/Blast");
            _laserBeam = Resources.Load<AudioClip>("Audio/LaserBeam");
            _mixerGroup = Resources.Load<AudioMixer>("Audio/Mixer");
            //_buttonHighlight = Resources.Load<AudioClip>("Audio/ButtonHighlight");
            _buttonClick = Resources.Load<AudioClip>("Audio/ButtonClick");

            _audioBackground.outputAudioMixerGroup = _mixerGroup.FindMatchingGroups("Music")[0];
            _audioBackground.loop = true;

            _audioExplosion.outputAudioMixerGroup = _mixerGroup.FindMatchingGroups("Effects/Explosion")[0];
            _audioExplosion.playOnAwake = false;
            _audioExplosion.volume = 0.4f;                                                              // Magic
            _audioExplosion.loop = false;

            _audioFirstGun.outputAudioMixerGroup = _mixerGroup.FindMatchingGroups("Effects/FirstGun")[0];
            _audioFirstGun.playOnAwake = false;
            _audioFirstGun.volume = 0.3f;                                                              // Magic
            _audioFirstGun.loop = false;

            _audioSecondGun.outputAudioMixerGroup = _mixerGroup.FindMatchingGroups("Effects/SecondGun")[0];
            _audioSecondGun.playOnAwake = false;
            _audioSecondGun.clip = _laserBeam;
            _audioSecondGun.volume = 0.7f;                                                              // Magic
            _audioSecondGun.loop = false;

            _audioUI.outputAudioMixerGroup = _mixerGroup.FindMatchingGroups("UI")[0];
            _audioUI.playOnAwake = false;
            _audioUI.volume = 0.7f;                                                              // Magic
            _audioUI.loop = false;
        }

        private void Start()
        {
            //StartInit();
        }

        private void Update()
        {
            //Debug.Log(_eventSystem.isFocused);                                                  //+++++++++++++++++++++++++++
        }

        public void PlayBackgroundMusic(string pathMusic)
        {
            _audioBackground.clip = Resources.Load<AudioClip>(pathMusic);
            _audioBackground.Play();
        }

        public void PlaySoundBlastShot()
        {
            _audioFirstGun.PlayOneShot(_blast);
        }

        public void PlaySoundLaserBeamShoting(bool isActiveLaserBeam)
        {
            if (isActiveLaserBeam)
            {
                _audioSecondGun.Play();
            }
            else
            {
                _audioSecondGun.Stop();
            }
        }

        public void PlaySoundExplosion()
        {
            _audioExplosion.PlayOneShot(_explosion2);
        }

        //public void PlaySoundOnButtonHighlighted()              // Пекределать под UI
        //{
        //    _audioExplosion.PlayOneShot(_buttonHighlight);
        //}

        public void PlaySoundOnButtonClick()                    // Переделать под UI
        {
            _audioFirstGun.PlayOneShot(_buttonClick);
        }

        private void StartInit()            // Не нужно?
        {
            PlaySoundBlastShot();
            _audioFirstGun.Stop();
            PlaySoundExplosion();
            _audioSecondGun.Stop();
            PlaySoundLaserBeamShoting(true);
            PlaySoundLaserBeamShoting(false);
        }
    }
}