using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Interactive
    {
        [SerializeField] private LaserPresenter _laser;
        private AudioSource _audioSorce;           // Вынести в отдельную Audio систему
        private AudioClip _explosion;           // Вынести в отдельную Audio систему
        private AudioClip _blast;           // Вынести в отдельную Audio систему

        private ShipModel _shipModel;
        private ShipMovement _shipMovement;

        protected override void Awake()
        {
            base.Awake();
            StartInit();

            _audioSorce = GetComponent<AudioSource>();           // Вынести в отдельную Audio систему
            _explosion = Resources.Load<AudioClip>("Audio/Explode");           // Вынести в отдельную Audio систему
            _blast = Resources.Load<AudioClip>("Audio/Blast");           // Вынести в отдельную Audio систему
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UserInput.MovementStarted += _shipMovement.OnMovementStart;
            UserInput.MovementCanceled += _shipMovement.OnMovementCancel;
            UserInput.RotationStarted += _shipMovement.OnRotationStart;
            UserInput.RotationCanceled += _shipMovement.OnRotationCancel;
            UserInput.ShootingFromFirstGunStarted += _shipModel.FirstGun.OnShootingStart;
            UserInput.ShootingFromFirstGunCanceled += _shipModel.FirstGun.OnShootingCancel;
            UserInput.ShootingFromSecondGunStarted += _shipModel.SecondGun.OnShootingStart;
            UserInput.ShootingFromSecondGunCanceled += _shipModel.SecondGun.OnShootingCancel;

            Updated += _shipModel.FirstGun.Tick;
            Updated += _shipModel.SecondGun.Tick;
            _shipModel.FirstGun.Shot += PlaySoundBlastShot;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UserInput.MovementStarted -= _shipMovement.OnMovementStart;
            UserInput.MovementCanceled -= _shipMovement.OnMovementCancel;
            UserInput.RotationStarted -= _shipMovement.OnRotationStart;
            UserInput.RotationCanceled -= _shipMovement.OnRotationCancel;
            UserInput.ShootingFromFirstGunStarted -= _shipModel.FirstGun.OnShootingStart;
            UserInput.ShootingFromFirstGunCanceled -= _shipModel.FirstGun.OnShootingCancel;
            UserInput.ShootingFromSecondGunStarted -= _shipModel.SecondGun.OnShootingStart;
            UserInput.ShootingFromSecondGunCanceled -= _shipModel.SecondGun.OnShootingCancel;

            Updated -= _shipModel.FirstGun.Tick;
            Updated -= _shipModel.SecondGun.Tick;
            _shipModel.FirstGun.Shot += PlaySoundBlastShot;
        }

        protected override void OnPauseMenuPresed(bool isPaused)
        {
            base.OnPauseMenuPresed(isPaused);

            if (isPaused)
            {
                Updated -= _shipModel.FirstGun.Tick;
                Updated -= _shipModel.SecondGun.Tick;
            }
            else
            {
                Updated += _shipModel.FirstGun.Tick;
                Updated += _shipModel.SecondGun.Tick;
            }
        }

        public void SetPresentersFactory(PresentersFactory factory) => ((DefaultGun)_shipModel.FirstGun).SetFactory(factory);       // Чтобы не прокидывать фабрику, оружее тоже вынести в префаб?
        public float GetAngleRotation() => _shipModel.RotationAngle;
        public void SubscribeOnSecondGunCharge(Action<float> func) => ((LaserGun)_shipModel.SecondGun).ChargingTimer.Changed += func;
        public void SubscribeOnSecondGunNumberChargesChange(Action<int> func) => ((LaserGun)_shipModel.SecondGun).NumberOfLaserCharges.Changed += func;

        private void StartInit()
        {
            _shipModel = new ShipModel(this, _laser);
            _shipMovement = new ShipMovement(_shipModel);
            SetModel(_shipModel);
            SetModelMovement(_shipMovement);
            SetOverlapLayer(LayerMask.NameToLayer(Config.EnemyLayerName));
            ((LaserGun)_shipModel.SecondGun).Shot += OnLaserShoot;
        }

        private void OnLaserShoot(bool isActivate)
        {
            if (isActivate)
                SetDegreesPerSecond(Config.PlayerRotationSpeed / 3);                // Magic
            else
                SetDegreesPerSecond(Config.PlayerRotationSpeed);
        }

        private void PlaySoundBlastShot()           // Вынести в отдельную Audio систему
        {
            _audioSorce.PlayOneShot(_blast);
        }

        private void PlaySoundLaserBeamShoting(bool isActiveLaserBeam)           // Вынести в отдельную Audio систему
        {
            // Добавить непрерывный звук лазера, который будет тартовать на true и прерыватся на false
        }
    }
}