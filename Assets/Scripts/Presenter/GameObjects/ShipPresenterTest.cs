using UnityEngine;

namespace Asteroids
{
    public class ShipPresenterTest : PresenterTest
    {
        [SerializeField] private ProjectilePresenter _prefabProjectile;

        private ShipModel _shipModel;
        private ShipMovementTest _shipMovement;
        private RootController _userInput;

        private void Awake()
        {
            _userInput = new RootController();
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;
            _shipModel = new ShipModel(startPosition, 0f, _prefabProjectile);
            _shipMovement = new ShipMovementTest(/*_shipModel*/);
            _shipMovement.SetModel(_shipModel);

            SetModel(_shipModel);
            SetMovement(_shipMovement);
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

        public Vector2 GetPosition()
        {
            return _shipModel.Position;
        }
    }
}