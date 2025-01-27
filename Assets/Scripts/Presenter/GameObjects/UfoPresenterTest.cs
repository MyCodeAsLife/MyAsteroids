using UnityEngine;

namespace Asteroids
{
    public class UfoPresenterTest : PresenterTest
    {
        private const string LayerEnemy = "Player";

        [SerializeField] private ShipPresenterTest _playerShip;

        private void Awake()
        {
            StartInit();
        }

        private void StartInit()
        {
            var position = new Vector2(0.1f, 0.1f);
            var startPosition = position * Config.ScaleWindowSize;

            var viewMovement = new ViewMovement(transform);
            SetViewMovement(viewMovement);
            var objModel = new UfoModelTest();
            var _objMovement = new UfoMovementTest(objModel, _playerShip);
            SetModel(objModel);
            SetModelMovement(_objMovement);
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            objModel.Position = startPosition;
            objModel.DegreesPerSecond = 100f;
            objModel.MovementSpeed = 0.003f;
            objModel.MaxMovementSpeed = 0.003f;
        }
    }
}