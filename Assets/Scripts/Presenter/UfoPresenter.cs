using UnityEngine;

namespace Asteroids
{
    public class UfoPresenter : Presenter
    {
        // Почти все переместить в Presenter и зависимые методы
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ShipPresenter _playerShip;

        private Transform _enemyView;
        private Transformable _enemyModel;
        private Movement _enemyMovement;


        private Vector2 _displaySize;
        private Vector2 _offsetPosition;

        private void Start()
        {
            _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            _offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
            var center = new Vector2(0.1f, 0.1f);
            var startPosition = center * Config.ScaleWindowSize;
            _enemyModel = new UfoModel(startPosition, 0f);
            _enemyMovement = new UfoMovement(_enemyModel, _displaySize, _playerShip);
            //_enemyView = Resources.Load<Transform>("Prefabs/Ship");                     // EnemyPresenter переделать под конкретный Presenter?
            //_enemyView = Instantiate(_enemyView, transform.parent);
            //_enemyView.gameObject.SetActive(true);
            _enemyView = transform;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            MoveShipModel(deltaTime);
            MoveShipView(deltaTime);
        }

        private void MoveShipModel(float deltaTime)
        {
            _enemyMovement.Tick(deltaTime);
        }

        private void MoveShipView(float deltaTime)
        {
            var correctPosition = (_displaySize * _enemyModel.Position) - _offsetPosition;
            _enemyView.localPosition = correctPosition;
            _enemyView.rotation = Quaternion.Euler(0f, 0f, _enemyModel.RotationAngle);
        }
    }
}