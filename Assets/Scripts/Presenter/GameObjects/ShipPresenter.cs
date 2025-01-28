using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Presenter
    {
        private const string LayerEnemy = "Enemy";

        [SerializeField] private PresentersFactory _factory;        // Через интерфейс?
        [SerializeField] Transform _firePoint;

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

        public Vector2 GetPosition()
        {
            return _shipModel.Position;
        }

        public float GetAngleRotation()
        {
            return _shipModel.RotationAngle;
        }

        private void StartInit()
        {
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;

            _shipModel = new ShipModel(_factory, this);
            _userInput = new RootController();
            _shipMovement = new ShipMovement(_shipModel);
            SetModel(_shipModel);
            SetModelMovement(_shipMovement);
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            _shipModel.Position = startPosition;
            _shipModel.DegreesPerSecond = 180f;
            _shipModel.MovementSpeed = 0.001f;
            _shipModel.MaxMovementSpeed = 0.001f;
        }
    }
}