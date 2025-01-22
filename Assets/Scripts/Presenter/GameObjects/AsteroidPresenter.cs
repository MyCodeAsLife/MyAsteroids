using Asteroids;
using UnityEngine;

public class AsteroidPresenter : Presenter
{
    // ѕочти все переместить в Presenter и зависимые методы
    //[SerializeField] private Canvas _canvas;
    //// Ётот блок переметить в Presenter?
    //private Transform _objectView;
    //private Transformable _objectModel;
    //private Movement _objectMovement;    // перед включением астероида, передать ему стартовую позицию(передает фабрика котора€ его генерирует), сченерированную на фабрике объектов

    //private Vector2 _startPosition;
    //private Vector2 _displaySize;
    //private Vector2 _offsetPosition;

    private void Awake()
    {
        //_displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
        //_offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
        var center = new Vector2(0.5f, 0.5f);
        var startPosition = center * Config.ScaleWindowSize;
        var _objectModel = new AsteroidModel(startPosition, 0f, Config.AsteroidCost);
        SetModel(_objectModel);
        SetMovement(new AsteroidMovement(_objectModel/*, _displaySize*/));
        //_objectMovement.SetDisplaySize(_displaySize);
        //_objectMovement.SetMovementSpeed(Config.AsteroidMaxSpeed);
        //_objectMovement.SetRotationSpeed(200f);
        //_objectMovement.SetDirectionOfMovement(_objectModel.Forward);
        //_objectMovement.SetDirectionOfRotation(1);
        //_enemyView = Resources.Load<Transform>("Prefabs/Ship");                     // EnemyPresenter переделать под конкретный Presenter?
        //_enemyView = Instantiate(_enemyView, transform.parent);
        //_enemyView.gameObject.SetActive(true);
        //_objectView = transform;

        SetStartPosition(startPosition);
        SetOverlapLayer(LayerMask.NameToLayer("Player"));
    }

    //private void Update()        // ѕереместить в Presenter?
    //{
    //    float deltaTime = Time.deltaTime;

    //    MoveObjectModel(deltaTime);
    //    MoveObjectView(deltaTime);
    //}

    //private void MoveObjectModel(float deltaTime)        // ѕереместить в Presenter?
    //{
    //    _objectMovement.Tick(deltaTime);
    //}

    //private void MoveObjectView(float deltaTime)        // ѕереместить в Presenter
    //{
    //    var correctPosition = (_displaySize * _objectModel.Position) - _offsetPosition;
    //    _objectView.localPosition = correctPosition;
    //    _objectView.rotation = Quaternion.Euler(0f, 0f, _objectModel.RotationAngle);
    //}

    //private void SetStartPosition(Vector2 position)
    //{
    //    _startPosition = position;
    //}
}
