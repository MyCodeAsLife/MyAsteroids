using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipPresenter : Presenter
    {
        // ѕочти все переместить в Presenter и зависимые методы
        [SerializeField] private Canvas _canvas;

        private Transform _shipView;
        private Transformable _shipModel;
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
            _shipModel = new ShipModel(startPosition, 0f);
            _shipMovement = new ShipMovement(_shipModel, _displaySize);
            _shipView = Resources.Load<Transform>("Prefabs/Ship");
            _shipView = Instantiate(_shipView, transform.parent);
            _shipView.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _userInput.MovementStarted += _shipMovement.OnMovementStart;
            _userInput.MovementCanceled += _shipMovement.OnMovementCancel;
            _userInput.RotationStarted += _shipMovement.OnRotationStart;
            _userInput.RotationCanceled += _shipMovement.OnRotationCancel;
            _userInput.ShootingFromFirstGunStarted += OnShootingStart;
            _userInput.ShootingFromFirstGunCanceled += OnShootingCancel;
            _userInput.ShootingFromSecondGunStarted += OnShootingStart;
            _userInput.ShootingFromSecondGunCanceled += OnShootingCancel;
        }

        private void OnDisable()
        {
            _userInput.MovementStarted -= _shipMovement.OnMovementStart;
            _userInput.MovementCanceled -= _shipMovement.OnMovementCancel;
            _userInput.RotationStarted -= _shipMovement.OnRotationStart;
            _userInput.RotationCanceled -= _shipMovement.OnRotationCancel;
            _userInput.ShootingFromFirstGunStarted -= OnShootingStart;
            _userInput.ShootingFromFirstGunCanceled -= OnShootingCancel;
            _userInput.ShootingFromSecondGunStarted -= OnShootingStart;
            _userInput.ShootingFromSecondGunCanceled -= OnShootingCancel;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            MoveModel(deltaTime);
            MoveView(deltaTime);
        }

        public Vector2 GetPosition()
        {
            return _shipModel.Position;
        }

        private void OnShootingCancel()
        {
            throw new NotImplementedException();
        }

        private void OnShootingStart()
        {
            throw new NotImplementedException();
        }

        private void MoveModel(float deltaTime)
        {
            _shipMovement.Tick(deltaTime);
        }

        private void MoveView(float deltaTime)
        {
            var correctPosition = (_displaySize * _shipModel.Position) - _offsetPosition;
            _shipView.localPosition = correctPosition;
            _shipView.rotation = Quaternion.Euler(0f, 0f, _shipModel.RotationAngle);
        }
    }
}