using UnityEngine;

namespace Asteroids
{
    public class UfoPresenter : Presenter
    {
        [SerializeField] private ShipPresenter _playerShip;                 // После тетсирования удалить

        private void Awake()
        {
            StartInit();
        }

        public void SetAtackTarget(ShipPresenter playerShip) => ((UfoMovement)ModelMovement).SetTarget(playerShip);

        private void StartInit()        // После тестирования, удалить все кроме создания классов
        {
            var position = new Vector2(0.1f, 0.1f);                         // На время тестирования
            var startPosition = position * Config.PlayerExistenceLimit;     // На время тестирования
            var objModel = new EnemyModel(Config.UfoCost);                  // На время тестирования

            SetModel(objModel);
            var modelMovement = new UfoMovement(objModel);
            SetModelMovement(modelMovement);
            SetAtackTarget(_playerShip);
            SetOverlapLayer(LayerMask.NameToLayer(Config.PlayerLayerName));

            objModel.Position = startPosition;                              // На время тестирования
            objModel.DegreesPerSecond = 100f;                               // На время тестирования
            objModel.MovementSpeed = 0.003f;                                // На время тестирования
            objModel.MaxMovementSpeed = 0.003f;                             // На время тестирования
        }
    }
}