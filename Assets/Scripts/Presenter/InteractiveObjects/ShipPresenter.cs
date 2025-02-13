using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Interactive
    {
        [SerializeField] private LaserPresenter _laser;

        private ShipModel _shipModel;
        private ShipMovement _shipMovement;
        private RootAudioSystem _audioSystem;

        protected override void Awake()
        {
            base.Awake();
            StartInit();
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
            _shipModel.FirstGun.Shot += _audioSystem.PlaySoundBlastShot;
            ((LaserGun)_shipModel.SecondGun).Shot += _audioSystem.PlaySoundLaserBeamShoting;
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
            _shipModel.FirstGun.Shot -= _audioSystem.PlaySoundBlastShot;
            ((LaserGun)_shipModel.SecondGun).Shot -= _audioSystem.PlaySoundLaserBeamShoting;
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

        public void SetPresentersFactory(PresentersFactory factory) => ((DefaultGun)_shipModel.FirstGun).SetFactory(factory);       // ����� �� ����������� �������, ������ ���� ������� � ������?
        public float GetAngleRotation() => _shipModel.RotationAngle;
        public void SubscribeOnSecondGunCharged(Action<float> func) => ((LaserGun)_shipModel.SecondGun).ChargingTimer.Changed += func;
        public void SubscribeOnSecondGunNumberChargesChanged(Action<int> func) => ((LaserGun)_shipModel.SecondGun).NumberOfLaserCharges.Changed += func;
        public void SubscribeOnSecondGunMaxNumberChargesChanged(Action<int> func) => ((LaserGun)_shipModel.SecondGun).MaxNumberOfLaserCharges.Changed += func;

        private void StartInit()
        {
            _shipModel = new ShipModel(this, _laser);
            _shipMovement = new ShipMovement(_shipModel);
            SetModel(_shipModel);
            SetModelMovement(_shipMovement);
            SetOverlapLayer(LayerMask.NameToLayer(Config.EnemyLayerName));
            ((LaserGun)_shipModel.SecondGun).Shot += OnLaserShoot;
            _audioSystem = FindFirstObjectByType<RootAudioSystem>();
            Destroyed += OnEndGame;
        }

        private void OnLaserShoot(bool isActivate)
        {
            if (isActivate)
                SetDegreesPerSecond(Config.PlayerRotationSpeed / 3);                // Magic
            else
                SetDegreesPerSecond(Config.PlayerRotationSpeed);
        }

        private void OnEndGame(Interactive obj)
        {
            gameObject.SetActive(false);
            GameState.EndGame();                    // EndGame-�� ���������� �� destroy ������
        }
    }
}