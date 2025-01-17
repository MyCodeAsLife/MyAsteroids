using Asteroids;
using UnityEngine;

public class AsteroidPresenter : Presenter
{
    // Почти все переместить в Presenter и зависимые методы
    [SerializeField] private Canvas _canvas;

    private Transform _enemyView;
    private Transformable _enemyModel;
    private Movement _enemyMovement;    // перед включением астероида, передать ему стартовую позицию(передает фабрика которая его генерирует), сченерированную на фабрике объектов

    private Vector2 _startPosition;
    private Vector2 _displaySize;
    private Vector2 _offsetPosition;

    private void Start()
    {
        _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
        _offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
        var center = new Vector2(0.5f, 0.5f);
        var startPosition = center * Config.ScaleWindowSize;
        _enemyModel = new AsteroidModel(startPosition, 0f, Config.AsteroidCost);
        _enemyMovement = new AsteroidMovement(_enemyModel, _displaySize);
        _enemyMovement.SetMovementSpeed(Config.AsteroidSpeed);
        _enemyMovement.SetRotationSpeed(200f);
        _enemyMovement.SetDirectionOfMovement(_enemyModel.Forward);
        _enemyMovement.SetDirectionOfRotation(1);
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

    private void SetStartPosition(Vector2 position)
    {
        _startPosition = position;
    }
}
