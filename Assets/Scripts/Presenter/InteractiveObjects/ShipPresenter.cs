using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Interactive
    {
        [SerializeField] LaserPresenter _laser;

        private ShipModel _shipModel;
        private ShipMovement _shipMovement;
        private RootController _userInput;

        private void Awake()
        {
            StartInit();
        }

        private void OnEnable()
        {
            _userInput.MovementStarted += _shipMovement.OnMovementStart;
            _userInput.MovementCanceled += _shipMovement.OnMovementCancel;
            _userInput.RotationStarted += _shipMovement.OnRotationStart;
            _userInput.RotationCanceled += _shipMovement.OnRotationCancel;
            _userInput.ShootingFromFirstGunStarted += _shipModel.FirstGun.OnShootingStart;
            _userInput.ShootingFromFirstGunCanceled += _shipModel.FirstGun.OnShootingCancel;
            _userInput.ShootingFromSecondGunStarted += _shipModel.SecondGun.OnShootingStart;
            _userInput.ShootingFromSecondGunCanceled += _shipModel.SecondGun.OnShootingCancel;
        }

        private void OnDisable()
        {
            _userInput.MovementStarted -= _shipMovement.OnMovementStart;
            _userInput.MovementCanceled -= _shipMovement.OnMovementCancel;
            _userInput.RotationStarted -= _shipMovement.OnRotationStart;
            _userInput.RotationCanceled -= _shipMovement.OnRotationCancel;
            _userInput.ShootingFromFirstGunStarted -= _shipModel.FirstGun.OnShootingStart;
            _userInput.ShootingFromFirstGunCanceled -= _shipModel.FirstGun.OnShootingCancel;
            _userInput.ShootingFromSecondGunStarted -= _shipModel.SecondGun.OnShootingStart;
            _userInput.ShootingFromSecondGunCanceled -= _shipModel.SecondGun.OnShootingCancel;
        }

        private new void Update()
        {
            _shipModel.FirstGun.Tick(Time.deltaTime);
            _shipModel.SecondGun.Tick(Time.deltaTime);
            base.Update();
        }

        public void SetPresentersFactory(PresentersFactory factory) => ((DefaultGun)_shipModel.FirstGun).SetFactory(factory);       // Чтобы не прокидывать фабрику, оружее тоже вынести в префаб?
        public float GetAngleRotation() => _shipModel.RotationAngle;
        public void SubscribeOnSecondGunCharge(Action<float> func) => ((LaserGun)_shipModel.SecondGun).ChargingTimer.Changed += func;
        public void SubscribeOnSecondGunNumberChargesChange(Action<int> func) => ((LaserGun)_shipModel.SecondGun).NumberOfLaserCharges.Changed += func;

        private void StartInit()
        {
            _shipModel = new ShipModel(this, _laser);
            _userInput = new RootController();
            _shipMovement = new ShipMovement(_shipModel);
            SetModel(_shipModel);
            SetModelMovement(_shipMovement);
            SetOverlapLayer(LayerMask.NameToLayer(Config.EnemyLayerName));
            ((LaserGun)_shipModel.SecondGun).Shot += OnLaserShoot;
        }

        private void OnLaserShoot(bool isActivate)
        {
            if (isActivate)
                SetDegreesPerSecond(Config.PlayerRotationSpeed / 3);
            else
                SetDegreesPerSecond(Config.PlayerRotationSpeed);
        }
    }
}