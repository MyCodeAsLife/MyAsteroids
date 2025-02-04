using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Presenter
    {
        //[SerializeField] private PresentersFactory _factory;        // Через интерфейс?
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

        private void StartInit()
        {
            //var center = new Vector2(0.5f, 0.5f);
            //var startPosition = center * Config.PlayerExistenceLimit;

            _shipModel = new ShipModel(this, _laser);
            _userInput = new RootController();
            _shipMovement = new ShipMovement(_shipModel);
            SetModel(_shipModel);
            SetModelMovement(_shipMovement);
            SetOverlapLayer(LayerMask.NameToLayer(Config.EnemyLayerName));

            //_shipModel.Position = startPosition;
            //_shipModel.DegreesPerSecond = Config.PlayerRotationSpeed;
            //_shipModel.MovementSpeed = Config.PlayerShipMovementSpeed;
            //_shipModel.MaxMovementSpeed = Config.PlayerShipMaxMovementSpeed;
        }
    }
}