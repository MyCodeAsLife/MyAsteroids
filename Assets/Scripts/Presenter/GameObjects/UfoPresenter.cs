using UnityEngine;

namespace Asteroids
{
    public class UfoPresenter : Presenter
    {
        private const string LayerEnemy = "Player";

        [SerializeField] private ShipPresenter _playerShip;         // Получать из фабрики или спавнера?

        private void Awake()
        {
            StartInit();
        }

        private void StartInit()
        {
            var position = new Vector2(0.1f, 0.1f);
            var startPosition = position * Config.ScaleWindowSize;
            var objModel = new EnemyModel(Config.UfoCost);

            SetModel(objModel);
            SetModelMovement(new UfoMovement(objModel, _playerShip));
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            objModel.Position = startPosition;
            objModel.DegreesPerSecond = 100f;
            objModel.MovementSpeed = 0.003f;
            objModel.MaxMovementSpeed = 0.003f;
        }
    }
}