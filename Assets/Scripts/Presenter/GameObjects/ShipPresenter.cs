using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Presenter
    {
        // Почти все переместить в Presenter и зависимые методы
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _shipView;
        [SerializeField] private ProjectilePresenter _prefabProjectile;
        // Этот блок переметить в Presenter?
        private ShipModel _shipModel;
        private ShipMovement _shipMovement;
        private RootController _userInput;

        private Vector2 _displaySize;
        private Vector2 _offsetPosition;

        private void Awake()
        {
            _userInput = new RootController();
            _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            _offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;
            _shipModel = new ShipModel(startPosition, 0f, _prefabProjectile);
            _shipMovement = new ShipMovement(_shipModel/*, _displaySize*/);
            _shipMovement.SetDisplaySize(_displaySize);
            //_shipView = Resources.Load<Transform>("Prefabs/Ship");
            //_shipView = Instantiate(_shipView, transform.parent);
            //_shipView.gameObject.SetActive(true);
            SetOverlapLayer(LayerMask.NameToLayer("Enemy"));
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

        private void Update()        // Переместить в Presenter?
        {
            float deltaTime = Time.deltaTime;

            MoveModel(deltaTime);
            MoveView(deltaTime);
            _shipModel.FirstGun.Tick(deltaTime);
            _shipModel.SecondGun.Tick(deltaTime);

            CollisionCheck();
        }

        public Vector2 GetPosition()
        {
            return _shipModel.Position;
        }

        private void MoveModel(float deltaTime)        // Переместить в Presenter?
        {
            _shipMovement.Tick(deltaTime);
        }

        private void MoveView(float deltaTime)        // Переместить в Presenter
        {
            var correctPosition = (_displaySize * _shipModel.Position) - _offsetPosition;
            _shipView.localPosition = correctPosition;
            _shipView.rotation = Quaternion.Euler(0f, 0f, _shipModel.RotationAngle);
        }
    }
}