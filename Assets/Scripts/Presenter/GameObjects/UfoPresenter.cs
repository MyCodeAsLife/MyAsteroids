using UnityEngine;

namespace Asteroids
{
    public class UfoPresenter : Presenter
    {
        [SerializeField] private ShipPresenter _playerShip;                 // ����� ������������ �������

        private void Awake()
        {
            StartInit();
        }

        public void SetAtackTarget(ShipPresenter playerShip) => ((UfoMovement)ModelMovement).SetTarget(playerShip);

        private void StartInit()        // ����� ������������, ������� ��� ����� �������� �������
        {
            var position = new Vector2(0.1f, 0.1f);                         // �� ����� ������������
            var startPosition = position * Config.PlayerExistenceLimit;     // �� ����� ������������
            var objModel = new EnemyModel(Config.UfoCost);                  // �� ����� ������������

            SetModel(objModel);
            var modelMovement = new UfoMovement(objModel);
            SetModelMovement(modelMovement);
            SetAtackTarget(_playerShip);
            SetOverlapLayer(LayerMask.NameToLayer(Config.PlayerLayerName));

            objModel.Position = startPosition;                              // �� ����� ������������
            objModel.DegreesPerSecond = 100f;                               // �� ����� ������������
            objModel.MovementSpeed = 0.003f;                                // �� ����� ������������
            objModel.MaxMovementSpeed = 0.003f;                             // �� ����� ������������
        }
    }
}