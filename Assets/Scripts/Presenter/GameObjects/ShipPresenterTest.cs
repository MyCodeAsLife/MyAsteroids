using UnityEngine;

namespace Asteroids
{
    public class ShipPresenterTest : PresenterTest
    {
        private const string LayerEnemy = "Enemy";

        private ShipModelTest _shipModel = new();
        private ShipMovementTest _shipMovement = new();
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

        public Vector2 GetPosition()
        {
            return _shipModel.Position;
        }

        private void StartInit()
        {
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;

            var viewMovement = new ViewMovement(transform);
            SetViewMovement(viewMovement);
            _userInput = new RootController();
            _shipMovement.SetModel(_shipModel);
            _shipMovement._inertiaSimulator.SetModel(_shipModel);
            SetModel(_shipModel);
            SetModelMovement(_shipMovement);
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            _shipModel.Position = startPosition;
            _shipModel.RotationAngle = 0f;
            _shipModel.DegreesPerSecond = 180f;
            _shipModel.MovementSpeed = 0.003f;
            _shipModel.MaxMovementSpeed = 0.003f;
        }
    }
}